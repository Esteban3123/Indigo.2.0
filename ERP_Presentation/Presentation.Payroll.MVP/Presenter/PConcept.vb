'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 16-07-2013
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
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de conceptos
''' </summary>
Public Class PConcept

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IConcept
    ''' </summary>
    Private _view As IConcept
    ''' <summary>
    ''' Variable que se utilizapa para tratar el tipo de vinculacion como un Objeto
    ''' </summary>
    Private _concept As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable para la lista de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupList As List(Of Group)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de conceptos
    ''' </summary>
    ''' <param name="view">Vista de conceptos</param>
    Public Sub New(ByRef view As IConcept)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar la rejilla con los grupos de los conceptos.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function Load_ConceptsGroup() As Task
        'Genero una lista de todos los grupos
        Using Model As New MGroups(MGroups.TAG)
            GroupList = Await Model.ListAllGroupsAsync
        End Using

        Dim concept As Concept = _view.ConceptObject

        For Each itemConceptGroup As ConceptGroup In concept.ConceptGroup
            itemConceptGroup.Apply = True
        Next

        If GroupList IsNot Nothing Then
            For Each item As Group In GroupList
                If concept.ConceptGroup.Where(Function(x) x.GroupId = item.Id).Count = 0 Then
                    Dim conceptGroup = New ConceptGroup()
                    conceptGroup.Apply = False
                    conceptGroup.Group = item
                    concept.ConceptGroup.Add(conceptGroup)
                End If
            Next
        End If

        _view.DatasourceConceptGroup = concept.ConceptGroup
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para Cargar el GridLookUpEdit
    ''' </summary>
    Public Async Sub Initializes()


    End Sub

    Public Function LoadConceptAdjustment(ConceptClass As String) As List(Of PayrollConceptXpo)
        Using Model As New MGroups(MGroups.TAG)
            Dim filtroConsulta As String = "ConceptClass = '" & ConceptClass & "' AND ConceptType = 2"
            Return XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollConceptXpo)(Nothing, filtroConsulta)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la configuracion de los parametros de mensajeria
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns></returns>
    Public Async Function GetGeneralLedgerSettings(ByVal idOperatingUnit As Integer) As Task(Of Domain.Entities.GeneralLedgerSettings)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSettingAccountAsync(idOperatingUnit)
    End Function


#End Region
End Class



