'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/11/2014
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
Imports Infrastructure.Data.Xpo
Imports System.ServiceModel
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports DevExpress.Xpo

#End Region

Public Class PGroupersCareGroup

#Region "Variables"



    Dim View As IGroupersCareGroup

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IGroupersCareGroup)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub InitilizeCups()
        Using model As New MBusqueda
            View.CUPSXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatus(True)
        End Using
    End Sub

    Public Sub InitilizeGroupers()
        Using model As New MBusqueda
            View.GroupersXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListGroupersByStatus(True)
        End Using
    End Sub

    Public Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer) As GroupersCareGroup
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetGroupersCareGroup(CareGroupId, grouperId, Me.Indigo.AuditMessageWcf)
    End Function

    Public Sub GetAllAGACTIMED()
        Using model As New MBusqueda
            View.ActivitiesXpo = XpoServiceEx.Instance(Me.Indigo.HisContainer).CrystalService.GetAllAGACTIMED()
        End Using
    End Sub
#End Region

End Class
