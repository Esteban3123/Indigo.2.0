#Region "Imports"

Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Practices.Unity

#End Region

''' <summary>
''' Provee metodos de extensiòn específicos de Windows (DevExpress, Unity)
''' </summary>
Public Module ExtentionsWindows

    <Extension()>
    Public Function GetRowSearch(gridView As GridView) As Object
        Dim obj As Object = gridView.GetFocusedRow()
        Do While TypeOf obj Is DevExpress.Data.NotLoadedObject
            System.Windows.Forms.Application.DoEvents()
            obj = gridView.GetFocusedRow()
        Loop
        Return obj
    End Function

#Region "Assembly Extensions"
    ''' <summary>
    ''' Obtiene los tipos cargables de un Assembly de forma segura
    ''' </summary>
    <Extension()>
    Private Function GetLoadableTypes(assembly As Assembly) As IEnumerable(Of Type)
        Try
            Return assembly.GetTypes()
        Catch ex As ReflectionTypeLoadException
            Return ex.Types.Where(Function(t) t IsNot Nothing)
        Catch
            Return Enumerable.Empty(Of Type)()
        End Try
    End Function
#End Region

#Region "Injector"
    Public Class ModuleInjector
        Public Sub Load(ByVal container As IUnityContainer, type As Type)
            Dim implementedTypes = AppDomain.CurrentDomain.GetAssemblies() _
                .Where(Function(m) m.FullName.StartsWith("Application.") _
                    OrElse m.FullName.StartsWith("Domain.") _
                    OrElse m.FullName.StartsWith("Infrastructure.Data.")) _
                .SelectMany(Function(m) m.GetLoadableTypes()) _
                .Where(Function(m) Not m.IsInterface AndAlso Not m.IsGenericType AndAlso type.IsAssignableFrom(m))

            For Each item As Type In implementedTypes
                Dim interfaces = item.GetInterfaces()? _
                    .Where(Function(m) Not m.Equals(type) AndAlso Not m.Equals(GetType(IDisposable)))? _
                    .ToList()
                If interfaces IsNot Nothing AndAlso interfaces.Any() Then
                    interfaces.ForEach(Sub(i) container.RegisterType(i, item))
                End If
            Next
        End Sub
    End Class
#End Region

End Module
