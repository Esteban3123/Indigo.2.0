'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-04-17
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class SearchMedicalOrderRecipeArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna la orden medica seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property MedicalOrderRecipe As String

    ''' <summary>
    ''' Identificacion del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientIdentification As String

    ''' <summary>
    ''' Tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeIdentification As Integer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()

    End Sub

#End Region

End Class
