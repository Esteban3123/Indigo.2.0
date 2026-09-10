'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IPortfolioLawyer

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un abogado por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLawyerByCode(code As String, audit As AuditMessage) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' Obtiene un abogado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLawyerById(id As Integer) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' metodo para guardar un abogado
    ''' </summary>    
    <OperationContract()>
    Function SaveLawyer(Lawyer As Lawyer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Lawyer)

    ''' <summary>
    ''' metodo para eliminar un abogado
    ''' </summary>    
    <OperationContract()>
    Function DeleteLawyer(Lawyer As Lawyer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateLawyer(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Lawyer)

#End Region

End Interface
