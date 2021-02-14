window.L.Toolbar2.EditAction.Control.Undo = window.L.Toolbar2.Action.extend({
    options: {
        toolbarIcon: { html: 'Undo' }
    },
    initialize: function(map, featureGroup, editing) {
        this.editing = editing;
        window.L.Toolbar2.Action.prototype.initialize.call(this);
    },
    addHooks: function() {
        this.editing.revertLayers();
        this.editing.disable();
    }
});

