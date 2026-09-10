'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface ILawyerAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para obtener un abogado por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetLawyerByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' Obtiene un abogado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLawyerById(id As Integer) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' metodo para guardar un abogado
    ''' </summary>    
    Function SaveLawyer(ByVal Lawyer As Lawyer, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' metodo para eliminar un abogado
    ''' </summary>    
    Function DeleteLawyer(ByVal Lawyer As Lawyer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateLawyer(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Lawyer)

End Interface
