#Michael Parker
#8/6/2014
#http://www.reddit.com/r/dailyprogrammer/comments/2crqml/8062014_challenge_174_intermediate_forum_avatar/

#Forum Avatar Generator
#Takes a name and generates a 128 x 128 avatar png, similar to GitHub avatars

#Uses the ChunkyPNG gem to create the image
#https://github.com/wvanbergen/chunky_png

#Usage: AvatarGenerator.rb "<username>"

require 'digest'
require 'chunky_png'

if ARGV.length < 1
	puts "No name given"	
else
	#Get MD5 hash for the username entered, and get integer value of each byte
	hash = Digest::MD5.hexdigest(ARGV[0])
	bytes = hash.bytes

	#Get color RGB values from three of the bytes of the hash
	color = [bytes[7], bytes[5], bytes[12]]

	
	#Go through each character in the hash and if it is letter a-t, put a color pixel(1)
	#and if not, put an inactive pixel(0)
	img = []
	hash.each_char {|char|
		if char =~ /[a-t]/
			img.push(1)
		else
			img.push(0)
		end
	}

	#Create image bitmap
	bitmap = []
	for i in 1..5 do 
		current_line = img.first(5)		#Get the first five bits
		current_line = current_line + current_line.reverse	#Add the reversed line to the current line for y axis reflection
		bitmap.push(current_line)		#Add the line to the bitmap
		img = img.drop(5)				#Remove the five bits used
	end

	#Appends the reversed bitmap to the original, essentially reflecting it over the x axis
	bitmap = bitmap + bitmap.reverse

	#Image creation
	img = ChunkyPNG::Image.new(10,10, ChunkyPNG::Color::WHITE)

	#Use the bitmap created earlier to make the image
	#Iterate through the bitmap and set the pixels of the image to the appropriate color
	primary_color = ChunkyPNG::Color.rgba(color[0], color[1], color[2], 128)
	for i in 0..9 do
		current = bitmap[i]
		for j in 0..9 do
			if current[j] == 1
				img[i,j] = primary_color
			end
		end
	end

	#Resize the image to an appropriate size
	img = img.resize(128,128)
	img.save(ARGV[0]+".png", :interlace => true)
end
