# CMDB - Cineaste Movie Database

Jag och en kursare har skapat en webbsida för filmälskare. Här jobbar vi emot två API:er - ett för att hämta information om filmer via OMDB API och sedan använder vi ID:et för att hämta information om antal likes eller dislikes i CMDB API:et eller lägga till en like eller dislike för det specifika ID:et.


## Tekniker

Projektet är skapat i Visual Studio med hjälp av ramverket ASP.NET core och enligt MVC-mönstret.

Här jobbar vi även med vymodeller för att underlätta separationen mellan logiken och det grafiska. 

### Startsida

När du öppnar startsidan möts du av den film med flest likes/dislikes.
<img width="964" src="https://github.com/samuelstrom93/Movie-Cineaste-Database/blob/master/Resources/front-page-1.png">

Här kan du filtrera topplistan på olika sätt med flera olika parametrar.

<img width="964" src="https://github.com/samuelstrom93/Movie-Cineaste-Database/blob/master/Resources/front-page.png">

### Asynkrona anrop 
För att inte ladda om sidan vid klick på like/dislike-knapp

```
function like() {
    fetch(originUrl + '/api/movie/' + imdbID + '/like')
        .then((response) => response.json())
        .then(function (data) {
            document.querySelector('#likes').textContent = data.numberOfLikes;
            likeBtn.disabled = true;
            dislikeBtn.disabled = true;
            likeBtn.style.opacity = "0.3";
            dislikeBtn.style.opacity = "0.3";
            let likeText = document.createTextNode(`You liked "${movieTitle}"!`);
            newText.appendChild(likeText);
            newText.style.color = "green";
            column.appendChild(newText);
    })
    .catch(function (error) {
        console.log(error);
    });
};
```

<img width="964" src="https://github.com/samuelstrom93/Movie-Cineaste-Database/blob/master/Resources/like.png">


### Sök

Här kan du filtrera dina sökningar enligt OMDB Api för filmer, serier, spel eller alla kategorier. Du kan även sortera dina sökningar via titel eller år. 
Sökningen visar antalet träffar och hur många sidor det blir totalt. För att bläddra mellan sidorna använder vi oss av LINQ med Take och Skip för att skapa en pagination-lösning:

```
switch (sortOrder)
            {
                case "Title":
                    searchViewModel.Search = searchViewModel.Search.OrderBy(x => x.Title).Skip(excludeRecords).Take(pageSize).ToList();
                    break;
                case "title_desc":
                    searchViewModel.Search = searchViewModel.Search.OrderByDescending(x => x.Title).Skip(excludeRecords).Take(pageSize).ToList(); 
                    break;
                case "Year":
                    searchViewModel.Search = searchViewModel.Search.OrderBy(x => x.Year).Skip(excludeRecords).Take(pageSize).ToList();
                    break;
                case "year_desc":
                    searchViewModel.Search = searchViewModel.Search.OrderByDescending(x => x.Year).Skip(excludeRecords).Take(pageSize).ToList(); 
                    break;
                default:
                    searchViewModel.Search = searchViewModel.Search.Skip(excludeRecords).Take(pageSize).ToList();
                    break;
            }
```


<img width="964" src="https://github.com/samuelstrom93/Movie-Cineaste-Database/blob/master/Resources/Sökfunktion.png">

