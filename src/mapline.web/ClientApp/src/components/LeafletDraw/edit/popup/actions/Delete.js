window.L.Toolbar2.EditAction.Popup.Delete = window.L.Toolbar2.Action.extend({
  options: {
    toolbarIcon: { className: 'leaflet-draw-edit-remove' }
  },

  initialize: function (map, shape, options) {
    this._map = map;
    this._shape = shape;

    window.L.Toolbar2.Action.prototype.initialize.call(this, map, options);
  },

  addHooks: function () {
    var map = this._map;

    map.removeLayer(this._shape);
    map.removeLayer(this.toolbar);

    console.log('firing draw:deleted');
    map.fire(window.L.Draw.Event.DELETED, { layers: window.L.layerGroup([this._shape]) });
  }
});
