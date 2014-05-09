/// <reference path="_references.views.js" />
/// <reference path="../webExpress/webExpress.controls.adpaters.js" />

var art = {};
art.ui = {};
art.ui.views = {};

(function () {
    art.ui.views.ArtistList = ArtistListClass;

    function ArtistListClass() {
        var _self = this;
        function _init() {
            _self.remove = remove;

        }

        function remove(artistId) {

        }

        _init();
    }
})();