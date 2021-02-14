window.L.Toolbar2.EditToolbar.Control = window.L.Toolbar2.Control.extend({
    options: {
        actions: [
            window.L.Toolbar2.EditAction.Control.Edit,
            window.L.Toolbar2.EditAction.Control.Delete
        ],
        className: 'leaflet-draw-toolbar',
    }
});
