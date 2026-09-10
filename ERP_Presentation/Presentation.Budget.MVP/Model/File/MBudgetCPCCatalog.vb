#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MBudgetCPCCatalog
    Inherits ModelBaseBudget
    Implements IDisposable

    Public Shared TAG As String = "2209"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tagform">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tagform As String)
        MyBase.New(tagform)
        TAG = tagform
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un rubro por código
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Public Async Function GetBudgetCPCCatalogByCode(Code As String) As Task(Of ActionResult(Of CPCCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetCPCCatalogByCodeAsync(Code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un Codigo catalogo CPC por Id
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Async Function GetBudgetCPCCatalogById(Id As Integer) As Task(Of ActionResult(Of CPCCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetCPCCatalogByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function



    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="CPCCatalog">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveBudgetCPCCatalogAsync(CPCCatalog As CPCCatalog) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.CPCCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveCPCCatalogAsync(CPCCatalog, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' cambiar estado de la entidad
    ''' </summary>
    ''' <param name="CPCCatalog">The CPCCatalog.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Async Function ChangeState(CPCCatalog As Domain.Entities.CPCCatalog, status As Boolean) As Task(Of ActionResult(Of CPCCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStatusCPCCatalogAsync(CPCCatalog, status, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CPCCatalog">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Async Function DeleteBudgetCPCCatalogAsync(CPCCatalog As CPCCatalog) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteCPCCatalogAsync(CPCCatalog, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' devuelve el numero de rubros existentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CountCPCCatalog() As Task(Of Integer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.CountCategoriesAsync(Me._indigoSessionValues.AuditMessageWcf)
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
