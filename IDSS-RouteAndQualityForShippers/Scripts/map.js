//http://jsfiddle.net/6RS2z/356/
    var vectorSource = new ol.source.Vector({
        //create empty vector
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

    var getStyle = function (quality) {
        if (quality > 70) {
            return styles[0];
        } else if (quality > 60) {
            return styles[1];
        } else if (quality >= 40) {
            return styles[2];
        } else {
            return styles[3];
        }
    };


    var createFeature = function (item) {
        var iconFeature = new ol.Feature({
            geometry: new
                ol.geom.Point(ol.proj.transform([item[0], item[1]], "EPSG:4326", "EPSG:3857")),
            name: item[2],
            quality: item[3],
            provisions: 'item[4]',
            water: 'item[5]',
            fuelOil: 'item[6]',
            fuelDiesel: 'item[7]',
            engine: 'item[8]',
            deck: 'item[9]',
            medical: 'item[10]',
            garbage: 'item[11]'

        });
        console.log("Long: " + item[0] + " Lat: " + item[1]);
        var quality = iconFeature.get('quality');
        iconFeature.setStyle(getStyle(quality));

        vectorSource.addFeature(iconFeature);
    };

    //@foreach(var item in Model)
    //{
    //    <text>
    //        var iconFeature = new ol.Feature({
    //            geometry: new
    //                            ol.geom.Point(ol.proj.transform([@item[0], @item[1]], 'EPSG:4326', 'EPSG:3857')),
    //                        name: '@item[2]',
    //                        quality: '@item[3]',
    //                        provisions: '@item[4]',
    //                        water: '@item[5]',
    //                        fuelOil: '@item[6]',
    //                        fuelDiesel: '@item[7]',
    //                        engine: '@item[8]',
    //                        deck: '@item[9]',
    //                        medical: '@item[10]',
    //                        garbage: '@item[11]'

    //                    });

    //                    var quality = iconFeature.get('quality');
    //                    iconFeature.setStyle(getStyle(quality));

    //                    vectorSource.addFeature(iconFeature);

    //                </text>
    //}



    //add the feature vector to the layer vector, and apply a style to whole layer
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: styles
    });

    var map = new ol.Map({
        layers: [new ol.layer.Tile({ source: new ol.source.OSM() }), vectorLayer],
        target: document.getElementById('map'),
        view: new ol.View({
            center: [0, 0],
            zoom: 2
        })
    });

    map.on("click", function (e) {
        map.forEachFeatureAtPixel(e.pixel, function (feature) {

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

