'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class MedicalFeesLiquidationRepository
    Inherits GenericRepository(Of MedicalFeesLiquidation)
    Implements IMedicalFeesLiquidationRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidation(code As String, medicalFeesContractId As Integer, optionConsult As Integer, Optional ByVal healthProfeesionalCode As String = Nothing) As MedicalFeesLiquidation Implements IMedicalFeesLiquidationRepository.GetMedicalFeesLiquidation
        Dim res As MedicalFeesLiquidation

        Select Case optionConsult
            Case 0 'Con 0 consulta por código
                res = (From d As MedicalFeesLiquidation In Me._context.MedicalFeesLiquidation
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
            Case 1 'Con 1 consulta por id del contrato
                res = (From d As MedicalFeesLiquidation In Me._context.MedicalFeesLiquidation
                   Where d.MedicalFeesContractId = medicalFeesContractId AndAlso d.Status = 1
                   Select d).FirstOrDefault
            Case 2 'Con 2 consulta por el código del médico
                res = (From d As MedicalFeesLiquidation In Me._context.MedicalFeesLiquidation
                  Where d.HealthProfessionalCode.Equals(healthProfeesionalCode.Trim) AndAlso d.Status = 1
                  Select d).FirstOrDefault
            Case Else
                res = Nothing
        End Select

        If res IsNot Nothing Then

            If res.MedicalFeesContractId IsNot Nothing Then
                Dim medicalFeesContract = (From mfc In _context.MedicalFeesContract.AsNoTracking Where mfc.Id = res.MedicalFeesContractId Select mfc).FirstOrDefault
                res.MedicalFeesContractDescription = medicalFeesContract.Code + " - " + medicalFeesContract.ContractName
            End If

            Dim filingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = res.FilingUnitId Select fu).FirstOrDefault
            res.FilingUnitDescription = filingUnit.Code + " - " + filingUnit.Name

            Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = res.SupplierTypeId Select st).FirstOrDefault
            res.SupplierTypeDescription = supplierType.Code + " - " + supplierType.Name

            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select cc).FirstOrDefault
                res.CostCenterDescription = costCenter.Code + " - " + costCenter.Name
            End If

            ''Se recorre los detalles para agregarla a la entidad MedicalFeesLiquidationDetail las propiedades extendidas para poder mostrar la info en la rejilla del formulario
            'If res.MedicalFeesLiquidationDetail IsNot Nothing AndAlso res.MedicalFeesLiquidationDetail.Count > 0 Then
            '    For Each itemDetail As MedicalFeesLiquidationDetail In res.MedicalFeesLiquidationDetail
            '        Dim medicalFeesCausation = (From mfc In _context.MedicalFeesCausation.AsNoTracking Where mfc.Id = itemDetail.MedicalFeesCausationId Select mfc).FirstOrDefault
            '        itemDetail.AdmissionNumber = medicalFeesCausation.AdmissionNumber
            '        itemDetail.PatientCode = medicalFeesCausation.PatientCode

            '        Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking Where tp.Id = medicalFeesCausation.ThirdPartyId Select tp).FirstOrDefault
            '        itemDetail.ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name

            '        Dim serviceOrder = (From so In _context.ServiceOrder.AsNoTracking Where so.Id = medicalFeesCausation.ServiceOrderId Select so).FirstOrDefault
            '        itemDetail.ServiceOrderCode = serviceOrder.Code

            '        Dim serviceOrderDetail = (From sod In _context.ServiceOrderDetail.AsNoTracking.Include("IPSService").AsNoTracking.Include("CUPSEntity").AsNoTracking Where sod.Id = medicalFeesCausation.ServiceOrderDetailId Select sod).FirstOrDefault
            '        If serviceOrderDetail.CUPSEntity IsNot Nothing Then
            '            itemDetail.IPSServiceName = serviceOrderDetail.CUPSEntity.Code + " - " + serviceOrderDetail.CUPSEntity.Description
            '        ElseIf serviceOrderDetail.IPSService IsNot Nothing Then
            '            itemDetail.IPSServiceName = serviceOrderDetail.IPSService.Code + " - " + serviceOrderDetail.IPSService.Name
            '        End If

            '        itemDetail.AmountPayable = medicalFeesCausation.AmountPayable
            '        itemDetail.InvoiceQuantity = medicalFeesCausation.InvoiceQuantity
            '        itemDetail.TotalAmountPayable = medicalFeesCausation.TotalAmountPayable
            '        itemDetail.MedicalFeesContractValue = medicalFeesCausation.MedicalFeesContractValue
            '        itemDetail.InvoiceReversal = medicalFeesCausation.InvoiceReversal
            '        itemDetail.StatusCausation = medicalFeesCausation.Status
            '        itemDetail.CausationDate = medicalFeesCausation.CausationDate
            '    Next
            'End If

            Select Case optionConsult
                Case 0 'Con 0 consulta por código
                    res.OriginalValue = (From g In _context.MedicalFeesLiquidation.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault
                Case 1 'Con 1 consulta por el id del contrato
                    res.OriginalValue = (From g In _context.MedicalFeesLiquidation.AsNoTracking
                                  Where g.MedicalFeesContractId = medicalFeesContractId
                                  Select g).FirstOrDefault
                Case 2 'Con 2 consulta por el código del médico
                    res.OriginalValue = (From g In _context.MedicalFeesLiquidation.AsNoTracking
                                  Where g.HealthProfessionalCode.Equals(healthProfeesionalCode.Trim)
                                  Select g).FirstOrDefault
            End Select

            Return res
        Else
            Return New MedicalFeesLiquidation()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una liquidacion por id del contrato profesional de la salud
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId As Integer) As MedicalFeesLiquidation Implements IMedicalFeesLiquidationRepository.GetMedicalFeesLiquidationByMedicalFeesContractId
        If MedicalFeesContractId = 0 Then
            Throw New ArgumentNullException("MedicalFeesContractId")
        End If
        Dim res = (From d As MedicalFeesLiquidation In Me._context.MedicalFeesLiquidation.Include("MedicalFeesLiquidationDetail")
                   Where d.MedicalFeesContractId = MedicalFeesContractId
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            'Dim medicalFeesContract = (From mfc In _context.MedicalFeesContract.AsNoTracking Where mfc.Id = res.MedicalFeesContractId Select mfc).FirstOrDefault
            'res.MedicalFeesContractDescription = medicalFeesContract.Code + " - " + medicalFeesContract.ContractName

            ''Se recorre los detalles para agregarla a la entidad MedicalFeesLiquidationDetail las propiedades extendidas para poder mostrar la info en la rejilla del formulario
            'If res.MedicalFeesLiquidationDetail IsNot Nothing AndAlso res.MedicalFeesLiquidationDetail.Count > 0 Then
            '    For Each itemDetail As MedicalFeesLiquidationDetail In res.MedicalFeesLiquidationDetail
            '        Dim medicalFeesCausation = (From mfc In _context.MedicalFeesCausation.AsNoTracking Where mfc.Id = itemDetail.MedicalFeesCausationId Select mfc).FirstOrDefault
            '        itemDetail.AdmissionNumber = medicalFeesCausation.AdmissionNumber
            '        itemDetail.PatientCode = medicalFeesCausation.PatientCode

            '        Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking Where tp.Id = medicalFeesCausation.ThirdPartyId Select tp).FirstOrDefault
            '        itemDetail.ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name

            '        Dim serviceOrder = (From so In _context.ServiceOrder.AsNoTracking Where so.Id = medicalFeesCausation.ServiceOrderId Select so).FirstOrDefault
            '        itemDetail.ServiceOrderCode = serviceOrder.Code

            '        itemDetail.AmountPayable = medicalFeesCausation.AmountPayable
            '        itemDetail.InvoiceQuantity = medicalFeesCausation.InvoiceQuantity
            '        itemDetail.TotalAmountPayable = medicalFeesCausation.TotalAmountPayable
            '        itemDetail.MedicalFeesContractValue = medicalFeesCausation.MedicalFeesContractValue
            '    Next
            'End If

            res.OriginalValue = (From g In _context.MedicalFeesLiquidation.AsNoTracking
                                  Where g.MedicalFeesContractId = MedicalFeesContractId
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New MedicalFeesLiquidation()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una liquidacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationById(id As Integer) As MedicalFeesLiquidation Implements IMedicalFeesLiquidationRepository.GetMedicalFeesLiquidationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.MedicalFeesLiquidation.AsNoTracking.Include("MedicalFeesLiquidationDetail").AsNoTracking Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            Return res.FirstOrDefault
        Else
            Return New MedicalFeesLiquidation()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cuenta contable de gastos de honorarios para generar los detalle de la cxp
    ''' </summary>
    ''' <param name="medicalFeesLiquidationDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountIdByMedicalFeesLiquidationDeatil(medicalFeesLiquidationDetail As MedicalFeesLiquidationDetail) As Integer Implements IMedicalFeesLiquidationRepository.GetMainAccountIdByMedicalFeesLiquidationDeatil

        'Dim medicalFeesCausation = (From mfc In _context.MedicalFeesCausation.AsNoTracking Where mfc.Id = medicalFeesLiquidationDetail.MedicalFeesCausationId Select mfc).FirstOrDefault
        'Dim serviceOrderDetail = (From sod In _context.ServiceOrderDetail.AsNoTracking Where sod.Id = medicalFeesCausation.ServiceOrderDetailId Select sod).FirstOrDefault
        'Dim cupsEntity = (From ce In _context.CUPSEntity.AsNoTracking Where ce.Id = serviceOrderDetail.CUPSEntityId Select ce).FirstOrDefault
        'Dim IPSServiceGroup = (From isg In _context.IPSServiceGroup.AsNoTracking Where isg.Id = cupsEntity.IPSServiceGroupId Select isg).FirstOrDefault
        'Return IPSServiceGroup.FeesExpensesAccountId

        Dim MainAccountId = (From mfc In _context.MedicalFeesCausation.AsNoTracking Where mfc.Id = medicalFeesLiquidationDetail.MedicalFeesCausationId
                             Join sod In _context.ServiceOrderDetail.AsNoTracking On mfc.ServiceOrderDetailId Equals sod.Id
                             Join ce In _context.CUPSEntity.AsNoTracking On sod.CUPSEntityId Equals ce.Id
                             Join isg In _context.BillingConcept.AsNoTracking On ce.BillingConceptId Equals isg.Id
                             Select isg.FeesExpensesAccountId).FirstOrDefault

        Return MainAccountId
    End Function

    ''' <summary>
    ''' Obtiene el id del centro de costo, pero primero consulta que la cuenta contable maneje centro de costo
    ''' </summary>
    ''' <param name="medicalFeesCausationId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterIdByMedicalFeesCausation(medicalFeesCausationId As Integer, mainAccountId As Integer) As Integer? Implements IMedicalFeesLiquidationRepository.GetCostCenterIdByMedicalFeesCausation
        'Se consulta que la cuenta contable maneje centro de costo
        Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = mainAccountId Select ma).FirstOrDefault
        If mainAccount.HandlesCostCenter Then
            Dim costCenter = (From mfc In _context.MedicalFeesCausation.AsNoTracking Where mfc.Id = medicalFeesCausationId
                                        Join sod In _context.ServiceOrderDetail.AsNoTracking On mfc.ServiceOrderDetailId Equals sod.Id
                                        Select sod.CostCenterId).FirstOrDefault
            Return costCenter

        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Valida que la cuenta contable que esta amarrada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId As Integer) As Boolean Implements IMedicalFeesLiquidationRepository.ValidateCostCenterBySupplierDistributionLineId
        'Se consulta la tabla de supplierDistributionLine por id
        Dim res = (From sdl In _context.SuppliersDistributionLines.AsNoTracking Where sdl.Id = supplierDistributionLineId
                                        Join dl In _context.DistributionLines.AsNoTracking On sdl.IdDistributionLine Equals dl.Id
                                        Join ma In _context.MainAccounts.AsNoTracking On dl.IdMainAccount Equals ma.Id
                                        Select ma.HandlesCostCenter).FirstOrDefault
        Return res
    End Function

    ''' <summary>
    ''' Guarda la liquidación de honorarios médicos
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveMedicalFeesLiquidation(xmlObject As String, codeUser As String) As SP_SaveMedicalFeesLiquidation_Result Implements IMedicalFeesLiquidationRepository.SP_SaveMedicalFeesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveMedicalFeesLiquidation(xmlObject, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Confirma la liquidación de honorarios médicos
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmMedicalFeesLiquidation(xmlObject As String, codeUser As String) As SP_ConfirmMedicalFeesLiquidation_Result Implements IMedicalFeesLiquidationRepository.SP_ConfirmMedicalFeesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmMedicalFeesLiquidation(xmlObject, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Anula la liquidacion de honorarios
    ''' </summary>
    ''' <param name="MedicalFeesLiquidationId"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_AnnularMedicalFeesLiquidation(MedicalFeesLiquidationId As Integer, codeUser As String) As SP_AnnularMedicalFeesLiquidation_Result Implements IMedicalFeesLiquidationRepository.SP_AnnularMedicalFeesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_AnnularMedicalFeesLiquidation(MedicalFeesLiquidationId, codeUser).SingleOrDefault
    End Function

#End Region

End Class
