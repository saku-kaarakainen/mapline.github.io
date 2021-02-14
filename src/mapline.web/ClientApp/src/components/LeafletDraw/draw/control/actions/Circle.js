window.L.Toolbar2.DrawAction.Circle = window.L.Toolbar2.DrawAction.fromHandler (
   window.L.Draw.Circle,
    {
        className: 'leaflet-draw-draw-circle',
        tooltip: window.L.drawLocal.draw.toolbar.buttons.circle
    },
    new window.L.Toolbar2({ actions: [window.L.Toolbar2.DrawAction.Cancel] })
);
