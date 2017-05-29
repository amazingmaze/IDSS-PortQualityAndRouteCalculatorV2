// Javascript code required for visualization of the map.
// some of the code borrowed from http://jsfiddle.net/6RS2z/356/
var createMap = function (model, map) {

    var vectorSource = new ol.source.Vector({
    });

    var stroke = new ol.style.Stroke({
        color: '#990000',
        width: 1.25
    });
    var styles = [
        new ol.style.Style({
            image: new ol.style.Circle({
                fill: new ol.style.Fill({
                    color: 'rgba(128,0,0,0.8)'
                }),
                radius: 5
            }),
            fill: new ol.style.Fill({
                color: 'rgba(128,0,0,0.8)'
            }),
            stroke: stroke
        }),
        new ol.style.Style({
            image: new ol.style.Circle({
                fill: new ol.style.Fill({
                    color: 'rgba(255,51,0,0.8)'
                }),
                radius: 5
            }),
            fill: new ol.style.Fill({
                color: 'rgba(255,51,0,0.8)'
            }),
            stroke: stroke
        }),
        new ol.style.Style({
            image: new ol.style.Circle({
                fill: new ol.style.Fill({
                    color: 'rgba(255,102,0,0.8)'
                }),
                radius: 5
            }),
            fill: new ol.style.Fill({
                color: 'rgba(255,102,0,0.8))'
            }),
            stroke: stroke
        }),
        new ol.style.Style({
            image: new ol.style.Circle({
                fill: new ol.style.Fill({
                    color: 'rgba(255,204,102,1)'
                }),
                radius: 5
            }),
            fill: new ol.style.Fill({
                color: 'rgba(255,204,102,1)'
            }),
            stroke: stroke
        })

    ];

    // Method to get different colors for the map-markers depending on the type of quality of the port
    var getStyle = function (quality) {
        if (quality > 70) {
            return styles[0];
        } else if (quality > 60) {
            return styles[2];
        } else if (quality >= 40) {
            return styles[3];
        } else {
            return styles[4];
        }
    };

    // Method to create a new feature (a feature is a marker on the map)
    var createFeature = function (item) {
        var iconFeature = new ol.Feature({
            geometry: new
                ol.geom.Point(ol.proj.transform([parseFloat(item.Longitude), parseFloat(item.Latitude)], "EPSG:4326", "EPSG:3857")),
            name: item.Name,
            quality: item.QualityScore,
            provisions: item.Provisions,
            water: item.Water,
            fuelOil: item.FuelOil,
            fuelDiesel: item.DieselOil,
            engine: item.Engine,
            deck: item.Deck,
            medical: item.MedicalFacilities,
            garbage: item.GarbageDisposal

        });
        var quality = iconFeature.get('quality');
        iconFeature.setStyle(getStyle(quality));
        vectorSource.addFeature(iconFeature);
    };

    var data = JSON.parse(model);
    data.forEach(function (item) {
        createFeature(item);
    });
    
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: styles
    });

    var map = new ol.Map({
        layers: [new ol.layer.Tile({ source: new ol.source.OSM() }), vectorLayer],
        target: map,
        view: new ol.View({
            center: [0, 0],
            zoom: 2
        })
    });

    // Create click functions to enable the user to select a port on a map
    map.on("click", function (e) {
        map.forEachFeatureAtPixel(e.pixel, function (feature, layer) {

            $("td.portName").text(feature.get('name'));
            $("td.portQuality").text(feature.get('quality'));
            if (feature.get('provisions') === "Y") {
                $("img.provisions").show();
            } else {
                $("img.provisions").hide();
            }
            if (feature.get('water') === "Y") {
                $("img.water").show();
            } else {
                $("img.water").hide();
            }
            if (feature.get('fuelOil') === "Y") {
                $("img.fuelOil").show();
            } else {
                $("img.fuelOil").hide();
            }
            if (feature.get('fuelDiesel') === "Y") {
                $("img.fuelDiesel").show();
            } else {
                $("img.fuelDiesel").hide();
            }
            if (feature.get('engine') === "Y") {
                $("img.engine").show();
            } else {
                $("img.engine").hide();
            }
            if (feature.get('deck') === "Y") {
                $("img.deck").show();
            } else {
                $("img.deck").hide();
            }
            if (feature.get('medical') === "Y") {
                $("img.medical").show();
            } else {
                $("img.medical").hide();
            }
            if (feature.get('garbage') === "Y") {
                $("img.garbage").show();
            } else {
                $("img.garbage").hide();
            }

        });
    });

}
