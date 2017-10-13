#!/usr/bin/env python3
# Show date and time via Unicorn Hat HD as binary.

import time
import datetime

try:
    import unicornhathd
    print("unicorn hat hd detected")
except ImportError:
    from unicorn_hat_sim import unicornhathd

print('Unicorn HAT HD Binary clock. Press Ctrl + C to exit.')

#unicornhathd.rotation(r=180)
unicornhathd.brightness(0.1)

# Constants
TIME_COLOR = (0,255,0)
HOUR_COLOR = (255,0,0)  #(0,0,0)
MINUTE_COLOR = (0,255,0)  #(0,0,0)
SECOND_COLOR = (255,0,255)  #(0,0,0)
BACKGROUND_COLOR = (0,153,255)      # RGB
BORDER_COLOR = (255,0,0)            # RGB
BINARY_COLOR = (255,0,0)            # RGB


def draw_background():  # Set every pixel to background color
    r, g, b = BACKGROUND_COLOR
    br, bg, bb = BORDER_COLOR  # Border RGB variables
    for x in range(16):
        for y in range(16):
            if (x != 0) and (x != 15) and (y != 0) and (y != 15):
                unicornhathd.set_pixel(x,y,r,g,b)
            #else:
                #unicornhathd.set_pixel(x,y,br,bg,bb)

def get_timeData():
    dt = datetime.datetime.now()
    timeData = {'year': dt.year, 'month': dt.month, 'day': dt.day,
                'hour': dt.hour, 'minute': dt.minute, 'second': dt.second}
    return timeData


def split_numbers(number):
    numberList = [int(x) for x in str(number)]
    return numberList


def split_values(): #dictionary
    storedDictionary = get_timeData()
    for i in storedDictionary:
        storedDictionary[i] = split_numbers(storedDictionary[i])
    #print(storedDictionary)
    return storedDictionary


def to_binary(number):
    assert type(number) is int
    binaryNumber = '{0:b}'.format(number)
    return binaryNumber

def construct_time_list(dictionary):
    timeList = []
    timeList.append(dictionary['hour'])
    timeList.append(dictionary['minute'])
    timeList.append(dictionary['second'])
    return timeList

def time_to_hat(list):
    #print(list)
    ledColor = ()
    columnIndex = 13
    rowIndex = 11
    for unit in range(len(list)):  # Loop through hours, minutes and seconds
        if unit == 0:
            ledColor = HOUR_COLOR
        elif unit == 1:
            ledColor = MINUTE_COLOR
        elif unit == 2:
            ledColor = SECOND_COLOR

        for binary in range(len(list[unit])):  # Single number in hour/minute/second
            for binaryDigit in list[unit][binary]:
                if binaryDigit == '1':
                    for x in range(2):  # Make one pixel 2x2 area
                        for y in range(2):
                            unicornhathd.set_pixel(columnIndex-x, rowIndex-y,
                                                   ledColor[0], ledColor[1],
                                                   ledColor[2])
                    rowIndex -= 2
                else:
                    rowIndex -= 2

            columnIndex -= 2
            rowIndex = 11



try:
    while True:
        #draw_background()
        timeData = split_values()
        for item in timeData:  # item in dictionary
            for i in range(len(timeData[item])):  # list in item
                timeData[item][i] = to_binary(timeData[item][i])
        timeList = construct_time_list(timeData)
        time_to_hat(timeList)
        unicornhathd.show()
        unicornhathd.clear()
        time.sleep(0.2)
except KeyboardInterrupt:
    unicornhathd.off()
