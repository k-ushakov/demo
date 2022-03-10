# Description
Build a REST service to measure distance in miles between two airports. Airports are identified by 3-letter IATA code.

## Sample call to get airport details:
GET https://xxx/airports/AMS HTTP/1.1

## Примеры информации об аэропорте:

https://xxx/airports/AMS

```json
{
	"country": "Netherlands",
	"city_iata": "AMS",
	"iata": "AMS",
	"city": "Amsterdam",
	"timezone_region_name": "Europe/Amsterdam",
	"country_iata": "NL",
	"rating": 3,
	"name": "Amsterdam",
	"location": {
		"lon": 4.763385,
		"lat": 52.309069
	},
	"type": "airport",
	"hubs": 7
}
```

https://xxx/airports/DME

```json
{
	"country": "Russian Federation",
	"city_iata": "MOW",
	"iata": "DME",
	"city": "Moscow",
	"timezone_region_name": "Europe/Moscow",
	"country_iata": "RU",
	"rating": 3,
	"name": "Domodedovo",
	"location": {
		"lon": 37.899494,
		"lat": 55.414566
	},
	"type": "airport",
	"hubs": 9
}
```

## api метод расчета расстояния:

http://localhost:5000/api/airport/distance/AMS/DME
