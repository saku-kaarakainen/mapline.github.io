window.L.Toolbar2.EditAction.Control.Edit = window.L.Toolbar2.EditAction.fromHandler(
  window.L.EditToolbar.Edit,
  {
    className: 'leaflet-draw-edit-edit',
    tooltip: 'Edit features'
  },
  new window.L.Toolbar2({
    actions: [
      window.L.Toolbar2.EditAction.Control.Save,
      window.L.Toolbar2.EditAction.Control.Undo
    ]
  })
);
