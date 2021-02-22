var movieCastViewModel = function (movieIdArg) {

    let self = this;

    self.addMode = ko.observable(false);
    self.movieId = ko.observable(movieIdArg);
    self.movieName = ko.observable();
    self.actorName = ko.observable("");
    self.cast = ko.observableArray([]);

    self.delete = function (actor) {
        if (confirm('Confirm the deletion of this actor from the cast?')) {
            var castMember = self.cast.find("name", actor.name);
            $.post('/api/castMember/' + castMember.actorId + '/delete')
                .done(function (result) {
                    var index = self.cast.remove(castMember);
                })
                .fail(function (result) {

                });
        }
    };
    
    self.addNew = function () {
        self.addMode(true);
    };
    
    self.save = function () {
        var postModel = {
            MovieId: self.movieId(),
            ActorName: self.actorName()
        };
        $.post('/api/castMember', postModel)
            .done(function (result) {
                self.cast.push({ actorId: result.actorId, name: self.actorName(), movieId: self.movieId() });
                self.addMode(false);
            })
            .fail(function (result) {
            });
    };
    
    self.cancel = function () {
        self.addMode(false);
    };

    ko.observableArray.fn.find = function (prop, data) {
        return ko.utils.arrayFirst(this(), function (item) {
            return item[prop] === data;
        });
    };

    $.get('/api/cast/' + self.movieId())
        .done(function (result) {
            self.movieName(result.movie.name);
            self.cast(result.cast);
        })
        .fail(function (result) {

        });

};