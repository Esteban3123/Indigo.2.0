'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MedicalFeesContractRepository
    Inherits GenericRepository(Of MedicalFeesContract)
    Implements IMedicalFeesContractRepository

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
    ''' Obtiene un contrato para liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContract(code As String) As MedicalFeesContract Implements IMedicalFeesContractRepository.GetMedicalFeesContract
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As MedicalFeesContract In Me._context.MedicalFeesContract.Include("MedicalFeesContractException")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.SupplierId IsNot Nothing Then
                'Se consulta el nombre del proveedor y de la linea de distribucion
                Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.SupplierDistributionLineId = sdl.Id Select sdl).FirstOrDefault
                Dim supplier = (From s In _context.Supplier.AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
                Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
                res.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
            End If

            If res.MedicalFeesContractException IsNot Nothing AndAlso res.MedicalFeesContractException.Count > 0 Then
                For Each item As MedicalFeesContractException In res.MedicalFeesContractException
                    Select Case item.ExceptionType
                        Case 1
                            If item.IPSServiceId IsNot Nothing Then
                                Dim ipsService = (From i In _context.IPSService.AsNoTracking Where i.Id = item.IPSServiceId Select i).FirstOrDefault
                                item.ExceptionDescription = ipsService.Code + " - " + ipsService.Name
                            End If
                        Case 2
                            If item.CUPSEntityId IsNot Nothing Then
                                Dim cupsEntity = (From c In _context.CUPSEntity.AsNoTracking Where c.Id = item.CUPSEntityId Select c).FirstOrDefault
                                item.ExceptionDescription = cupsEntity.Code + " - " + cupsEntity.Description
                            End If
                        Case 3
                            If item.CUPSSubgroupId IsNot Nothing Then
                                Dim cupsSubGroup = (From s In _context.CupsSubgroup.AsNoTracking Where s.Id = item.CUPSSubgroupId Select s).FirstOrDefault
                                item.ExceptionDescription = cupsSubGroup.Code + " - " + cupsSubGroup.Name
                            End If
                        Case 4
                            If item.CUPSGroupId IsNot Nothing Then
                                Dim cupsGroup = (From g In _context.CupsGroup.AsNoTracking Where g.Id = item.CUPSGroupId Select g).FirstOrDefault
                                item.ExceptionDescription = cupsGroup.Code + " - " + cupsGroup.Name
                            End If
                        Case 5
                            If item.CareGroupId IsNot Nothing Then
                                Dim careGroup = (From cg In _context.CareGroup.AsNoTracking Where cg.Id = item.CareGroupId Select cg).FirstOrDefault
                                item.ExceptionDescription = careGroup.Code + " - " + careGroup.Name
                            End If
                        Case 6
                            If item.ContractEntityId IsNot Nothing Then
                                Dim contractEntity = (From ce In _context.ContractEntity.AsNoTracking Where ce.Id = item.ContractEntityId Select ce).FirstOrDefault
                                item.ExceptionDescription = contractEntity.Code + " - " + contractEntity.Name
                            End If
                        Case 7
                            Select Case item.RateManualType
                                Case 1
                                    item.ExceptionDescription = "ISS 2001"
                                Case 2
                                    item.ExceptionDescription = "ISS 2004"
                                Case 3
                                    item.ExceptionDescription = "SOAT"
                            End Select
                        Case 8
                            item.ExceptionDescription = "General"
                    End Select
                    Select Case item.RateType
                        Case 1
                            item.RateDescription = item.PercentageRate.ToString + "%"
                        Case 2
                            If item.RateManualId IsNot Nothing Then
                                Dim rateManual = (From rm In _context.RateManual.AsNoTracking Where rm.Id = item.RateManualId Select rm).FirstOrDefault
                                item.RateManualDescription = rateManual.Code + " - " + rateManual.Name
                                item.RateDescription = item.RateManualDescription + " - " + item.RateVariation.ToString + "%"
                            End If
                        Case 3
                            item.RateDescription = String.Format("{0:C2}", item.AmountPayable)
                    End Select
                Next
            End If

            res.OriginalValue = (From g In _context.MedicalFeesContract.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New MedicalFeesContract()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un contrato para liquidacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContractById(id As Integer) As MedicalFeesContract Implements IMedicalFeesContractRepository.GetMedicalFeesContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.MedicalFeesContract.AsNoTracking.Include("MedicalFeesContractException").AsNoTracking Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            Return res.FirstOrDefault
        Else
            Return New MedicalFeesContract()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de todos los contratos profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListMedicalFeesContract() As List(Of MedicalFeesContract) Implements IMedicalFeesContractRepository.GetListMedicalFeesContract
        Dim listRes = (From mfc In _context.MedicalFeesContract.AsNoTracking.Include("MedicalFeesContractGeneral").AsNoTracking.Include("MedicalFeesContractGeneral.MedicalFeesContractGeneralException").AsNoTracking Select mfc).ToList
        Return listRes
    End Function

#End Region

End Class
