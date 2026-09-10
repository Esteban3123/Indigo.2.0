'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IRIPSPlane
    Inherits IDisposable

#Region "RIPS"

    Function AFStructure(data As List(Of SP_GenerateAFFileData_Result), Session As SessionValues) As StringBuilder

    Function USStructure(data As List(Of SP_GenerateUSFileData_Result)) As StringBuilder

    Function ACStructure(data As List(Of SP_GenerateACFileData_Result), ServiceCode As String) As StringBuilder

    Function ADStructure(data As List(Of SP_GenerateADFileData_Result), ServiceCode As String) As StringBuilder

    Function APStructure(data As List(Of SP_GenerateAPFileData_Result), ServiceCode As String) As StringBuilder

    Function ATStructure(data As List(Of SP_GenerateATFileData_Result), ServiceCode As String) As StringBuilder

    Function ANStructure(data As List(Of SP_GenerateANFileData_Result)) As StringBuilder

    Function AUStructure(data As List(Of SP_GenerateAUFileData_Result)) As StringBuilder

    Function AHStructure(data As List(Of SP_GenerateAHFileData_Result)) As StringBuilder

    Function AMStructure(data As List(Of SP_GenerateAMFileData_Result), CodificationType As String) As StringBuilder

    Function CTFile(ConsecutiveRadicateInvoice As String, ListPlaneRIPS As List(Of ActionMessageResult(Of StringBuilder))) As ActionMessageResult(Of StringBuilder)

#End Region

#Region "FURIPS"

    Function FURIPS1Structure(data As List(Of SP_GenerateFURIPS1FileData_Result)) As StringBuilder

    Function FURIPS2Structure(data As List(Of SP_GenerateFURIPS2FileData_Result)) As StringBuilder

    Function FURTRANStructure(data As List(Of SP_GenerateFURTRANFileData_Result)) As StringBuilder


#End Region

#Region "MegaRIPS"

    Function GetMegaRIPSByRadicateInvoiceId(ByVal radicateInvoiceId As Integer, companyCode As String, Optional invoicesList As List(Of RIPSBilling) = Nothing) As String

#End Region

#Region "MegaRIPS"

    Function MegaPlaneStructure(data As List(Of SP_GenerateMegaPlaneFileData_Result)) As StringBuilder

#End Region

End Interface
