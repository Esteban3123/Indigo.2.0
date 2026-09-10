'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
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
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de autorizacion de conceptos
''' </summary>
Public Class PAuthorizationConcept

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IAuthorizationConcept
    ''' </summary>
    Private _view As IAuthorizationConcept
    ''' <summary>
    ''' Variable que se utilizapa para tratar el tipo de vinculacion como un Objeto
    ''' </summary>
    Private _authorizationConcept As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Lista que contiene todos los conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private List_Concepts As List(Of Concept)

    ''' <summary>
    '''  lista de los conceptos autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private AuthorizationConcept_Datasource As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Objeto donde se tendra todos los conceptos authorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private List_AllAuthorizationConcept As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Lista que contiene los conceptos autorizados a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private List_To_Save As List(Of AuthorizationConcept)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de tipo de vinculación
    ''' </summary>
    ''' <param name="view">Vista de centros de estudio</param>
    Public Sub New(ByRef view As IAuthorizationConcept)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que inicializa los search look up
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function inizialites() As Task
        Using Model As New MEmployee(MEmployee.TAG)
            _view.Employee_Datasource = Await Model.ListAllEmployeeAsync
        End Using
        Using Model As New MGroups(MGroups.TAG)
            _view.Group_Datasource = Await Model.ListAllGroupsAsync
        End Using

        Using Model As New MConcept(MConcept.TAG)
            List_Concepts = Await Model.ListAllConceptAsync
        End Using
    End Function

    ''' <summary>
    ''' Carga los conceptos autorizados para un determinado grupo o empleado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function Load_AuthorizationConcept() As Task
        AuthorizationConcept_Datasource = New List(Of AuthorizationConcept)
        Me._view.List_AuthorizationConceptGroupIndex = New List(Of AuthorizationConcept)()
        Using Model As New MAuthorizationConcept(MAuthorizationConcept.TAG)
            List_AllAuthorizationConcept = Await Model.ListAllAuthorizationConceptAsync()
        End Using
        Dim _index As Integer = 0
        For Each _concept As Concept In List_Concepts
            Dim AuthConTemp As List(Of AuthorizationConcept)
            AuthConTemp = List_AllAuthorizationConcept.FindAll(Function(x) x.ConceptId = _concept.Id)
            If AuthConTemp.Count > 0 Then
                Dim objAuthoConcept As AuthorizationConcept
                objAuthoConcept = AuthConTemp.Item(0)

                If Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Group Then
                    If objAuthoConcept.AuthorizationConceptGroup.Where(Function(x) x.GroupId = _view.Group_Id).Count > 0 Then
                        objAuthoConcept.Apply = True
                    Else
                        Dim newAuthConceptGroup As AuthorizationConceptGroup = New AuthorizationConceptGroup
                        newAuthConceptGroup.GroupId = _view.Group_Id
                        objAuthoConcept.Apply = False
                        objAuthoConcept.AuthorizationConceptGroup.Add(newAuthConceptGroup)
                    End If

                ElseIf Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Employee Then
                    Dim _employee As Employee
                    Dim _employeeGroupId As Integer
                    Using Model As New MEmployee(MEmployee.TAG)
                        _employee = Await Model.GetEmployeeByIdAsync(Me._view.Employee_Id)
                    End Using
                    If _employee.Contract.Where(Function(x) x.Valid = True).Count > 0 Then
                        _employeeGroupId = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault().GroupId
                    End If
                    If objAuthoConcept.AuthorizationConceptGroup.Where(Function(x) x.GroupId = _employeeGroupId).Count > 0 Then
                        objAuthoConcept.Apply = True
                        Me._view.List_AuthorizationConceptGroupIndex.Add(objAuthoConcept)
                    ElseIf objAuthoConcept.AuthorizationConceptEmployee.Where(Function(x) x.EmployeeId = _view.Employee_Id).Count > 0 Then
                        objAuthoConcept.Apply = True
                    Else
                        Dim newAuthConceptEmployee As AuthorizationConceptEmployee = New AuthorizationConceptEmployee
                        newAuthConceptEmployee.EmployeeId = _view.Employee_Id
                        objAuthoConcept.Apply = False
                        objAuthoConcept.AuthorizationConceptEmployee.Add(newAuthConceptEmployee)
                    End If
                End If
                AuthorizationConcept_Datasource.Add(objAuthoConcept)
                _index += 1

            Else ' Si no existe esa autorizacion
                Dim newAuthConcept As New AuthorizationConcept
                If Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Group Then
                    Dim newAuthConceptGroup As New AuthorizationConceptGroup
                    newAuthConceptGroup.GroupId = _view.Group_Id
                    With newAuthConcept
                        .Apply = False
                        .Concept = _concept
                        .AuthorizationConceptGroup.Add(newAuthConceptGroup)
                    End With
                ElseIf Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Employee Then
                    Dim newAuthConceptBy As New AuthorizationConceptEmployee
                    newAuthConceptBy.EmployeeId = _view.Employee_Id
                    With newAuthConcept
                        .Apply = False
                        .Concept = _concept
                        .AuthorizationConceptEmployee.Add(newAuthConceptBy)
                    End With
                End If
                AuthorizationConcept_Datasource.Add(newAuthConcept)
                _index += 1
            End If
        Next
        If AuthorizationConcept_Datasource IsNot Nothing AndAlso AuthorizationConcept_Datasource.Count > 0 Then
            Me._view.AuthorizationCGridControl.DataSource = AuthorizationConcept_Datasource
            Me._view.ShowCheckAllControl = True
        End If
    End Function


    ''' <summary>
    ''' Metodo que contiene lógica de guardar los conceptos a autorizar
    ''' </summary>
    ''' <returns>Si se guardo existosamente los conceptos autorizados</returns>
    ''' <remarks></remarks>
    Public Async Function Save_AuthorizationConcept() As Task(Of Boolean)
        Dim indice As Integer = 0
        Dim _AuthConc As AuthorizationConcept
        While indice < AuthorizationConcept_Datasource.Count
            _AuthConc = AuthorizationConcept_Datasource.Item(indice)
            Dim idConcept = _AuthConc.ConceptId
            _AuthConc.Concept = Nothing
            _AuthConc.ConceptId = idConcept
            If _AuthConc.Apply = False Then
                If Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Group Then
                    Dim query = _AuthConc.AuthorizationConceptGroup.Where(Function(x) x.GroupId = _view.Group_Id)
                    If query.Count > 0 Then
                        query.FirstOrDefault().MarkAsDeleted()
                    End If
                ElseIf Me._view.AuthorizationConceptBy = EAuthorizationConceptBy.Employee Then
                    Dim query = _AuthConc.AuthorizationConceptEmployee.Where(Function(x) x.EmployeeId = _view.Employee_Id)
                    If query.Count > 0 Then
                        query.FirstOrDefault().MarkAsDeleted()
                    End If
                End If
                If _AuthConc.AuthorizationConceptEmployee.Count = 0 And _AuthConc.AuthorizationConceptGroup.Count = 0 Then
                    If _AuthConc.ChangeTracker.State = ObjectState.Added Then
                        AuthorizationConcept_Datasource.RemoveAt(indice)
                        indice -= 1
                    End If
                    _AuthConc.MarkAsDeleted()
                End If
            End If
            indice += 1
        End While

        Using Model As New MAuthorizationConcept(MAuthorizationConcept.TAG)
            Return Await Model.SaveListAuthorizationConceptAsync(AuthorizationConcept_Datasource)
        End Using
    End Function
#End Region

#Region "Enum"
    ''' <summary>
    ''' Enumeracion para determinar si se autoriazan conceptos por grupo o por empleado
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EAuthorizationConceptBy
        ''' <summary>
        ''' Grupo
        ''' </summary>
        ''' <remarks></remarks>
        Group = 1
        ''' <summary>
        ''' Empleado
        ''' </summary>
        ''' <remarks></remarks>
        Employee = 2
    End Enum
#End Region
End Class
