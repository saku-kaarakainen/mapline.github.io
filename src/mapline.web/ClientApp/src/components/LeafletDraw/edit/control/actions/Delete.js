window.L.Toolbar2.EditAction.Control.Delete = window.L.Toolbar2.EditAction.fromHandler(
  L.EditToolbar.Delete,
  {
    className: 'leaflet-draw-edit-remove',
    tooltip: 'Remove features'
  },
  new window.L.Toolbar2({
    actions: [
      window.L.Toolbar2.EditAction.Control.Save,
      window.L.Toolbar2.EditAction.Control.Undo
    ]
  })
);
