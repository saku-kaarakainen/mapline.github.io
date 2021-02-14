window.L.Toolbar2.EditToolbar.Popup = window.L.Toolbar2.Popup.extend({
	options: {
		actions: [
			window.L.Toolbar2.EditAction.Popup.Edit,
			window.L.Toolbar2.EditAction.Popup.Delete
		],
        className: 'leaflet-draw-toolbar'
	},

	onAdd: function (map) {
		var shape = this._arguments[1];

		if (shape instanceof window.L.Marker) {
			/* Adjust the toolbar position so that it doesn't cover the marker. */
			this.options.anchor = window.L.point(shape.options.icon.options.popupAnchor);
		}

		window.L.Toolbar2.Popup.prototype.onAdd.call(this, map);
	}
});
