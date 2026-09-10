'***********************************************************************
' Assembly         : Presentacion.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/08/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports Presentation.Taxes.MVP

#End Region

Public Class FrmSettingsTaxes
    'Implements ILaw441990

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Validar) = False
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Liquidar Impuestos")
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = False
    End Sub
#End Region

End Class