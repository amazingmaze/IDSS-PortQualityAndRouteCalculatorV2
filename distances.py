import requests
from bs4 import BeautifulSoup
import re
import time

d = {"REYKJAVIK": 24969,
	"NEW ORLEANS": 15151,
	"BUENOS AIRES": 26670,
	"LOS ANGELES": 14635,
	"HELSINGBORG": 19952,
	"AHUS": 19723,
	"OSAKA": 16005,
	"GEORGETOWN": 26114,
	"ABERDEEN": 25823,
	"BORDEAUX": 17967
	}

file = open("distances2.txt", "w")

locations = list(d.keys())
index = 0
# Create a FOR loop to go through each port
for loc in locations:
	index = index + 1
	new_idx = index
	for i in range(index,len(d.items())):
		print(loc + " : " + locations[new_idx])
		
		# Make the POST request
		time.sleep(1)
		url = "https://sea-distances.org/distances/ports/id_from/" + str(d.get(loc)) + "/id_to/" + str(d.get(locations[new_idx])) + "/speed/10"
		req = requests.post(url, data= {})
		# Get the nautical miles from the HTML respnse
		soup = BeautifulSoup(req.text, 'html.parser')
		rows = soup.findAll('td')

		print(rows[1].renderContents())
		distance = re.findall(r'\d+', str(rows[1].renderContents()))[0]
		# Store distance into a file
		# REYKJAVIK:NEW ORLEANS=4167
		file.write(loc + ":" + locations[new_idx] + "=" + distance + "\n");
		new_idx = new_idx + 1

file.close()





