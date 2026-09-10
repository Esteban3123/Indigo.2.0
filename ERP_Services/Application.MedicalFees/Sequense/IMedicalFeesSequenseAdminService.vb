'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IMedicalFeesSequenseAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    Function GetSequenseByIdForm(idForm As String) As MedicalFeesSecuence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenseGroupById(ByVal id As Int32) As List(Of String)

    Function SaveSequence(ByVal seq As MedicalFeesSecuence) As ActionResult

#End Region

End Interface
