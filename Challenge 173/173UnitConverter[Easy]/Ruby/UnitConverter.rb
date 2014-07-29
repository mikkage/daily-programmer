#Michael Parker
#Daily Programmer 7/28/14
#Unit Converter
#http://www.reddit.com/r/dailyprogrammer/comments/2bxntq/7282014_challenge_173_easy_unit_calculator/


#Conversion factors
length = {"in" => 1.0, "ft" => 12.0, "yd" => 36.0, "m" => 39.3701, "mi" => 63360.0, "atp" => 1.21483369}
mass = {"oz" => 1.0, "lb" => 16.0, "kg" => 35.274, "hogsheads" => 15545.2518}

puts "Supports length units in, ft, yd, m, mi, and atp"
puts "and mass units oz, lb, kg, hogsheads"
puts "Format: <value> <oldUnits> to <newUnits>"


input = "a"
until input.empty? do
	puts "Enter units to convert"
	input = gets.chomp

	if input.empty?
		puts "Bye"
		break
	end

	#Parse user input
	#Input format: 			<value> <fromUnit> to <newUnit>
	#Index after splitting:    0         1      2     3
	splitInput = input.split(' ')
	value = Float(splitInput.at(0))
	fromUnit = splitInput.at(1)
	toUnit = splitInput.at(3)

	#Make sure that the units are convertable before trying to convert them(both length or both mass)
	if ( (length.include? fromUnit) && (length.include? toUnit)) || ( (mass.include? fromUnit) && (mass.include? toUnit))
		if length.include? fromUnit
			#length conversion
			conversionFactor = (length[fromUnit] / length[toUnit])
		else
			#mass conversion
			conversionFactor = (mass[fromUnit] / mass[toUnit])
		end
		puts value * conversionFactor + " " + fromUnit
	else
		puts "Units do not match or are not valid\n"	
	end
end