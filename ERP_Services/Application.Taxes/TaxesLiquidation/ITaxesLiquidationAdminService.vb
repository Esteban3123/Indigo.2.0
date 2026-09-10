'***********************************************************************
' Assembly         : Application.Txes
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITaxesLiquidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda la liquidación de impuestos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, audit As AuditMessage) As ActionResult(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Confirma la liquidación de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Ids"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmTaxesLiquidation(Year As Integer, Ids As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, String, String, String))

End Interface
