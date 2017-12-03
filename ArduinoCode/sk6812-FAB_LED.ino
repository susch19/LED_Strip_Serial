
#include <FAB_LED.h>

// Declare the LED protocol and the port
sk6812<D, 6>  strip;

// How many pixels to control
const int numPixels = 94;

// How bright the LEDs will be (max 255)
const uint8_t maxBrightness = 60;

// The pixel array to display
grbw  pixels[numPixels] = {};

void setup()
{
	Serial.begin(921600);
	Serial.setTimeout(100);
	strip.clear(numPixels);
}

void set_all_leds(char color_r, char color_b, char color_g, char color_w)
{
	for (int pixel_index = 0; pixel_index < numPixels; pixel_index++)
	{
		pixels[pixel_index].r = color_r;
		pixels[pixel_index].b = color_g;
		pixels[pixel_index].g = color_b;
		pixels[pixel_index].w = color_w;
	}
	strip.sendPixels(numPixels, pixels);
}

void blink_leds(char color_r, char color_b, char color_g, char color_w)
{
	for (int i = 0; i < 3; i++)
	{
		set_all_leds(color_r, color_b, color_g, color_w);
		delay(200);
		strip.clear(numPixels);
		delay(200);
	}
}

char readBuffer[numPixels * 5];
void loop()
{
	if (Serial.available())
	{
		int count = Serial.read() * 5;
		Serial.println("ACK");

		int read_count = Serial.readBytes(readBuffer, count);

		if (read_count == 0)
			blink_leds(maxBrightness, 0, 0, 0);
		
		Serial.println("ACK");
		
		for (size_t i = 0; i < count; i++)
		{
			byte pos = readBuffer[i++];
			pixels[pos].g = readBuffer[i++];
			pixels[pos].r = readBuffer[i++];
			pixels[pos].b = readBuffer[i++];
			pixels[pos].w = readBuffer[i];
		}
		strip.sendPixels(numPixels, pixels);
	}
}
