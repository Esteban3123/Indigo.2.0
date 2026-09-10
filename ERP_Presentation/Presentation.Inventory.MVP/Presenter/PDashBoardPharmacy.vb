'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo

#End Region

Public Class PDashBoardPharmacy

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDashBoardPharmacy

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDashBoardPharmacy)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' carga las unidades funcionales
    ''' </summary>
    Public Sub LoadCareCenter()
        Me.View.CareCenterXPO = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Sub

End Class