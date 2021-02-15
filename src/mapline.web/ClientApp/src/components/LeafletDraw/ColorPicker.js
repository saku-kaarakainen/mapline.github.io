window.L.ColorPicker = window.L.Toolbar2.Action.extend({
  options: {
    toolbarIcon: { className: 'leaflet-color-swatch' }
  },

  initialize: function (map, shape, options) {
    this._map = map;
    this._shape = shape;

    window.L.setOptions(this, options);
    window.L.Toolbar2.Action.prototype.initialize.call(this, map, options);
  },

  addHooks: function () {
    this._shape.setStyle({ color: this.options.color });
    this.disable();
  },

  _createIcon: function (toolbar, container, args) {
    var colorSwatch = window.L.DomUtil.create('div'),
      width, height;

    window.L.Toolbar2.Action.prototype._createIcon.call(this, toolbar, container, args);

    window.L.extend(colorSwatch.style, {
      backgroundColor: this.options.color,
      width: window.L.DomUtil.getStyle(this._link, 'width'),
      height: window.L.DomUtil.getStyle(this._link, 'height'),
      border: '3px solid ' + window.L.DomUtil.getStyle(this._link, 'backgroundColor')
    });

    this._link.appendChild(colorSwatch);

    window.L.DomEvent.on(this._link, 'click', function () {
      this._map.removeLayer(this.toolbar.parentToolbar);
    }, this);
  }
});