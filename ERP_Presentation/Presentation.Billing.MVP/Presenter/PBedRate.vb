'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-06-2015
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
Imports Presentation.Payroll.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class PBedRate

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IBedRate

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IBedRate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    Public Sub GetAllStayType()
        Me.View.CODTIPESTXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllStayType()
    End Sub

    Public Sub GetAllCupsEntity()
        Me.View.GENCUPSXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatus(True)
        Me.View.GENCUPS2Xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatus(True)
    End Sub

    Public Sub GetAllCareCenter()
        Me.View.CODCENATEXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllCareCenter()
    End Sub

    Public Sub GetAllFunctionalUnit()
        Me.View.UFUCODIGOXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllFunctionalUnit()
    End Sub
#End Region

End Class