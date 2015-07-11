This project demonstrates how to read Microsoft PAL files.
There's little documentation out there around this and I think an example will really explain it in case you need it.

For me, I was converting images to Indexed Color in Photoshop and then needed save the colors in the image and import them to an app programatically.
Another way would be to load the image created in Photoshop and then read it and count the colors, but I figured this would be easier.

Used Photoshop CS6. Not sure that matters.

The main code to watch is in [PalReader.cs](https://code.google.com/p/pal-reader/source/browse/trunk/pal-reader/PAL%20Reader/PalReader.cs), where the file header is also explained in the class summary comments.