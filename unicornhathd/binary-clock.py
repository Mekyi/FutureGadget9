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

# Constants
BACKGROUND_COLOR = (0,153,255)      # RGB
BORDER_COLOR = (255,0,0)            # RGB
BINARY_COLOR = (255,0,0)            # RGB

def reset_board():  # Set every pixel to background color
    r, g, b = BACKGROUND_COLOR
    br, bg, bb = BORDER_COLOR  # Border RGB variables
    for x in range(16):
        for y in range(16):
            if (x != 0) and (x != 15) and (y != 0) and (y != 15):
                unicornhathd.set_pixel(x,y,r,g,b)
            else:
                unicornhathd.set_pixel(x,y,br,bg,bb)
                print(x, y)

def get_timeData():
    dt = datetime.datetime()
    timeData = {'year': dt.year(), 'month': dt.month(), 'day': dt.day(),
                'hour': dt.hour(), 'minute': dt.minute(), 'second': dt.second}
    return timeData

def convert_to_binary(dictionary):  # Converts dictionary items into binary'
    for i in range(len(dictionary)):
        dictionary[i]

try:
    #while True:
        reset_board()
        unicornhathd.show()
except KeyboardInterrupt:
    unicornhathd.off()
