'***********************************************************************
' Assembly         : Application.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAccountManagementSequenceAdminService
    Inherits IDisposable
#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    Function GetSequenseByIdForm(idForm As String) As AccountManagementSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenseGroupById(ByVal id As Int32) As List(Of String)


    Function SaveSequence(ByVal seq As AccountManagementSequence) As ActionResult

#End Region
End Interface
