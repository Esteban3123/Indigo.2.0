'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 15-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class MConcept
    Inherits ModelBase
    Implements IDisposable
    Private Indigo As SessionValues = SessionValues.Instance

#Region "Properties"

    Public Shared TAG As String = "549"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub
#End Region

#Region "methods"

    ''' <summary>
    ''' Obtener una lista de conceptos segun un criterio de lista de clase de conceptos
    ''' </summary>
    ''' <param name="listClassConcept">List de clases de conceptos</param>
    ''' <returns>El registro</returns>
    Public Async Function GetConceptByConceptClassAsync(ByVal listClassConcept As List(Of String)) As Task(Of List(Of Domain.Payroll.Entities.Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConceptByConceptClassAsync(listClassConcept, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por sucodigo modo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetConceptAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConceptAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por sucodigo 
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Function GetConcept(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConcept(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Function SaveConcept(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveConcept(reg, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveConceptAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveConceptAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Function DeleteConcept(ByVal reg As Domain.Payroll.Entities.Concept) As ActionMessageResult(Of Domain.Payroll.Entities.Concept)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteConcept(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteConceptAsync(ByVal reg As Domain.Payroll.Entities.Concept) As Task(Of ActionMessageResult(Of Domain.Payroll.Entities.Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteConceptAsync(reg, Indigo)
    End Function


    Public Async Function ListAllConceptAsync() As Task(Of List(Of Domain.Payroll.Entities.Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllConceptAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Concept", Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener las sucursales o sedes
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBranchAll() As Task(Of List(Of Domain.Entities.GlosasParametersInterface))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInterfacesParametersAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">contenedor de interfaz</param>
    ''' <returns>Lista de cuentas contables</returns>
    ''' <remarks></remarks>
    Public Async Function ListAccounts(ByVal container As String) As Task(Of List(Of SP_AccountsList_Result))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAccountsAsync(container, Me.Indigo)
    End Function


    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Public Async Function ListConceptAccountingStructure(ByVal InterfaceName As String, ByVal ConceptId As Integer) As Task(Of List(Of ConceptAccountingStructure))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAccountingStructureByInterfaceConceptIdAsync(InterfaceName, ConceptId, Me.Indigo)
    End Function

    ''' <summary>
    ''' Función Para Cargar la Estructura Contable de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAccountingStructure() As Task(Of List(Of Domain.Payroll.Entities.AccountingStructure))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllAccountingStructureAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateConcept(Code As String, State As Boolean) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateConceptAsync(Code, State, Indigo)
    End Function

    ''' <summary>
    ''' Lista los conceptos electronicos filtrados por tipo de concepto usando XPO
    ''' </summary>
    ''' <param name="conceptType">Tipo de concepto (1=Devengado, 2=Deducido, 3=Patronal)</param>
    ''' <returns>Lista de conceptos electrónicos XPO filtrados por tipo</returns>
    ''' <remarks></remarks>
    Public Function ListElectronicPayrollConceptsByType(ByVal conceptType As Integer) As List(Of PayrollElectronicPayrollConceptsXpo)
        Try
            Dim allConcepts = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollElectronicPayrollConceptsXpo)(Nothing, "State <> 0").ToList()
            If conceptType = 3 Then
                Return allConcepts
            Else
                Return allConcepts.Where(Function(c) c.ConceptType = conceptType OrElse c.ConceptType = 3).ToList()
            End If
        Catch ex As Exception
            Return New List(Of PayrollElectronicPayrollConceptsXpo)
        End Try
    End Function



#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
