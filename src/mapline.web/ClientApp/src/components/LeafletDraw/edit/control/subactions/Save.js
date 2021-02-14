window.L.Toolbar2.EditAction.Control.Save = window.L.Toolbar2.Action.extend({
    options: {
        toolbarIcon: { html: 'Save' }
    },
    initialize: function(map, featureGroup, editing) {
        this.editing = editing;
        window.L.Toolbar2.Action.prototype.initialize.call(this);
    },
    addHooks: function() {
        this.editing.save();
        this.editing.disable();
    }
});
