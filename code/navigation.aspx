<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="navigation.aspx.cs" Inherits="navigation" %>

<asp:content contentplaceholderid="ExtraStylesAndScripts" runat="server">    
     <link href="~/logo.ico" rel="shortcut icon" type="image/x-icon" />

    <script type='text/javascript'
            src='http://www.bing.com/api/maps/mapcontrol?callback=GetMap' 
            async defer></script>
    
            <script type='text/javascript'>
            var map, sessionKey, current, dest;


            var options = {
                enableHighAccuracy: true,
                timeout: 5000,
                maximumAge: 0
            };

            function success(pos) {
                current = pos.coords.latitude + ',' + pos.coords.longitude;
            };

            function error(err) {
                alert('Erro a obter localização atual.');
            };

            navigator.geolocation.getCurrentPosition(success, error, options);

            function GetMap()
            {
                map = new Microsoft.Maps.Map('#myMap', {
                    credentials: 'Ao5DoxLPTUFD-nXP2JQtkgfqszE2pPECL85KfHL8umoc9eg1jBnoCnOJnt8BQKih'
                });

                //Get a session key from the map to use with the REST services to make those requests non-billable transactions.
                map.getCredentials(function (c) {
                    sessionKey = c;
                    //Generate some routes

                    dest = "<%= MyProperty %>";
                    getRoute(current, dest, 'green');
                })
            }
            function getRoute(start, end, color) {


                //Calculate a route between the start and end points.
                var routeRequest = 'https://dev.virtualearth.net/REST/v1/Routes/Driving?wp.0=' + encodeURIComponent(start) + '&wp.1=' + encodeURIComponent(end) + '&ra=routePath&key=' + sessionKey;
                CallRestService(routeRequest, function (response) {
                    if (response &&
                       response.resourceSets &&
                       response.resourceSets.length > 0 &&
                       response.resourceSets[0].resources) {
                        var route = response.resourceSets[0].resources[0];
                        var routePath = route.routePath.line.coordinates;
                        //Generate an array of locations for the route path.
                        var locs = [];
                        for (var i = 0, len = routePath.length; i < len; i++) {
                            locs.push(new Microsoft.Maps.Location(routePath[i][0], routePath[i][1]));
                        }
                        //Draw the route line.
                        var line = new Microsoft.Maps.Polyline(locs, { strokeColor: color, strokeThickness: 3 });
                        map.entities.push(line);
                        //Add start and end pushpins.
                        var startLoc = new Microsoft.Maps.Location(route.routeLegs[0].actualStart.coordinates[0], route.routeLegs[0].actualStart.coordinates[1]);
                        var startPin = new Microsoft.Maps.Pushpin(startLoc, { icon: '/Common/images/startPin.png', anchor: new Microsoft.Maps.Point(2, 42) });
                        map.entities.push(startPin);
                        var endLoc = new Microsoft.Maps.Location(route.routeLegs[0].actualEnd.coordinates[0], route.routeLegs[0].actualEnd.coordinates[1]);
                        var endPin = new Microsoft.Maps.Pushpin(endLoc, { icon: '/Common/images/endPin.png', anchor: new Microsoft.Maps.Point(2, 42) });
                        map.entities.push(endPin);
                    }
                });
            }
            function CallRestService(request, callback) {
                if (callback) {
                    //Create a unique callback function.
                    var uniqueName = getUniqueName();
                    request += '&jsonp=' + uniqueName;
                    window[uniqueName] = function (response) {
                        callback(response);
                        delete (window[uniqueName]);
                    };
                    //Make the JSONP request.
                    var script = document.createElement("script");
                    script.setAttribute("type", "text/javascript");
                    script.setAttribute("src", request);
                    document.body.appendChild(script);
                }
            }
            function getUniqueName() {
                var name = '__callback' + Math.round(Math.random() * 100000);
                while (window[name]) {
                    name = '__callback' + Math.round(Math.random() * 100000);
                }
                return name;
            }
            </script>
</asp:content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div = "wrapper" style="height:100%">
        <div id="menu" style="float:left; width:30%; height:100%">
                <ul>
                    <li>Inicio</li>
                    <li>Histórico</li>
                    <li>Adicionar Etiquetas</li>
                    <li>Ver Histórico</li>
                    <li>Histórico de Avaliações</li>
                    <li>Autenticar</li>
                </ul>
        </div>



        <div id="interact" ClientIDMode="Static" runat="server">

            <div id="myMap" style="width:95%; height:500px; margin: 0 auto; margin-top:15px">
            </div>

        </div>
    
    </div>
</asp:Content>