var movieInfoViewModel = function (movieIdArg, movieNameArg, genreIdArg) {

    let self = this;

    self.editMode = ko.observable(false);
    self.movieId = ko.observable(movieIdArg);
    self.movieName = ko.observable(movieNameArg);
    self.selectedGenre = ko.observable();
    self.genres = ko.observableArray([]);

    self.edit = function () {
        self.editMode(true);
    };

    self.cancel = function() {
        self.editMode(false);
    };

    self.save = function () {
        var postModel = {
            MovieId: self.movieId(),
            Name: self.movieName(),
            GenreId: self.selectedGenre().genreId
        };

        $.post('/api/movieInfo', postModel)
            .done(function (result) {
                self.editMode(false);
            })
            .fail(function (result) {

            });        
    };

    ko.observableArray.fn.find = function (prop, data) {
        return ko.utils.arrayFirst(this(), function (item) {
            return item[prop] === data;
        });
    };

    $.get('/api/genres')
        .done(function (result) {
            self.genres(result);
            self.selectedGenre(ko.observable(self.genres.find("genreId", genreIdArg))()); 
        })
        .fail(function (result) {

        });

};