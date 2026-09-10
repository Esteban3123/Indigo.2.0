'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Extensiones de compatibilidad para ParameterCollection
'***********************************************************************

Imports System.Runtime.CompilerServices
Imports DevExpress.XtraReports.Parameters

Namespace Utilities

    ''' <summary>
    ''' Métodos de extensión para ParameterCollection (compatibilidad con versiones anteriores)
    ''' </summary>
    Public Module ParameterExtensions

        ''' <summary>
        ''' Obtiene un parámetro por nombre (reemplaza GetByName de versiones anteriores)
        ''' </summary>
        <Extension()>
        Public Function GetByName(collection As ParameterCollection, name As String) As Parameter
            If collection Is Nothing Then Return Nothing
            Return collection.Cast(Of Parameter)().FirstOrDefault(Function(p) p.Name = name)
        End Function

    End Module

End Namespace
