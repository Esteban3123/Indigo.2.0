Imports Domain.Entities
Imports Infrastructure.Data.Base
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports System.Linq.Expressions

Public Class LoadMassiveRepository
    Inherits GenericRepository(Of LoadMassive)
    Implements ILoadMassiveRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetLoadMassive(ByVal code As String) As LoadMassive Implements ILoadMassiveRepository.GetLoadMassive
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As LoadMassive In Me._context.LoadMassive.AsNoTracking().Include("LoadMassiveAccountPayable").AsNoTracking().Include("LoadMassiveAccountPayable.LoadMassiveAccountPayableDetail").AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
        If res IsNot Nothing Then
            If res.LoadMassiveAccountPayable IsNot Nothing AndAlso res.LoadMassiveAccountPayable.Count > 0 Then
                Dim dictionarySuppliers As New Dictionary(Of Integer, String)()
                Dim dictionaryDistributionLines As New Dictionary(Of Integer, DistributionLines)()
                Dim dictionaryMainAccounts As New Dictionary(Of Integer, String)()
                Dim dictionaryThirdParties As New Dictionary(Of Integer, String)()
                Dim dictionaryCostCenters As New Dictionary(Of Integer, String)()
                Dim dictionaryFilingUnits As New Dictionary(Of Integer, String)()
                Dim dictionarySupplierTypes As New Dictionary(Of Integer, String)()
                Dim dictionaryConcepts As New Dictionary(Of Integer, AccountPayableConcepts)()

                Dim codeName As String = String.Empty
                Dim distributionLine As DistributionLines = Nothing
                Dim accountPayableConcepts As AccountPayableConcepts = Nothing

                For Each ap In res.LoadMassiveAccountPayable
                    If Not dictionarySuppliers.ContainsKey(ap.SupplierId) Then
                        codeName = _context.Supplier.Where(Function(d) d.Id = ap.SupplierId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                        dictionarySuppliers.Add(ap.SupplierId, codeName)
                    Else
                        codeName = dictionarySuppliers(ap.SupplierId)
                    End If
                    ap.SupplierNit = codeName

                    If Not dictionaryDistributionLines.ContainsKey(ap.DistributionLineId) Then
                        distributionLine = _context.DistributionLines.Where(Function(d) d.Id = ap.DistributionLineId).FirstOrDefault()
                        dictionaryDistributionLines.Add(ap.DistributionLineId, distributionLine)
                    Else
                        distributionLine = dictionaryDistributionLines(ap.DistributionLineId)
                    End If
                    ap.DistributionLineCode = String.Format("{0} - {1}", distributionLine.Code, distributionLine.Name)

                    If Not dictionaryMainAccounts.ContainsKey(distributionLine.IdMainAccount) Then
                        codeName = _context.MainAccounts.Where(Function(d) d.Id = distributionLine.IdMainAccount).Select(Function(d) d.Number + " - " + d.Name).FirstOrDefault()
                        dictionaryMainAccounts.Add(distributionLine.IdMainAccount, codeName)
                    Else
                        codeName = dictionaryMainAccounts(distributionLine.IdMainAccount)
                    End If
                    ap.MainAccountNumber = codeName

                    If ap.CostCenterId IsNot Nothing Then
                        If Not dictionaryCostCenters.ContainsKey(ap.CostCenterId) Then
                            codeName = _context.CostCenter.Where(Function(d) d.Id = ap.CostCenterId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                            dictionaryCostCenters.Add(ap.CostCenterId, codeName)
                        Else
                            codeName = dictionaryCostCenters(ap.CostCenterId)
                        End If
                        ap.CostCenterCode = codeName
                    End If

                    If Not dictionaryFilingUnits.ContainsKey(ap.FilingUnitId) Then
                        codeName = _context.FilingUnit.Where(Function(d) d.Id = ap.FilingUnitId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                        dictionaryFilingUnits.Add(ap.FilingUnitId, codeName)
                    Else
                        codeName = dictionaryFilingUnits(ap.FilingUnitId)
                    End If
                    ap.FilingUnitCode = codeName

                    If Not dictionarySupplierTypes.ContainsKey(ap.SupplierTypeId) Then
                        codeName = _context.SupplierType.Where(Function(d) d.Id = ap.SupplierTypeId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                        dictionarySupplierTypes.Add(ap.SupplierTypeId, codeName)
                    Else
                        codeName = dictionarySupplierTypes(ap.SupplierTypeId)
                    End If
                    ap.SupplierTypeCode = codeName

                    If ap.AccountPayableId IsNot Nothing Then
                        ap.AccountPayableCode = _context.AccountPayable.Where(Function(d) d.Id = ap.AccountPayableId).Select(Function(d) d.Code).FirstOrDefault()
                    End If

                    Dim value As Decimal = 0
                    If ap.LoadMassiveAccountPayableDetail IsNot Nothing AndAlso ap.LoadMassiveAccountPayableDetail.Count > 0 Then
                        For Each apd In ap.LoadMassiveAccountPayableDetail
                            If Not dictionaryConcepts.ContainsKey(apd.AccountPayableConceptId) Then
                                accountPayableConcepts = _context.AccountPayableConcepts.Where(Function(d) d.Id = apd.AccountPayableConceptId).FirstOrDefault()
                                dictionaryConcepts.Add(apd.AccountPayableConceptId, accountPayableConcepts)
                            Else
                                accountPayableConcepts = dictionaryConcepts(apd.AccountPayableConceptId)
                            End If
                            apd.AccountPayableConceptCode = String.Format("{0} - {1}", accountPayableConcepts.Code, accountPayableConcepts.Name)

                            If Not dictionaryMainAccounts.ContainsKey(accountPayableConcepts.IdAccount) Then
                                codeName = _context.MainAccounts.Where(Function(d) d.Id = accountPayableConcepts.IdAccount).Select(Function(d) d.Number + " - " + d.Name).FirstOrDefault()
                                dictionaryMainAccounts.Add(accountPayableConcepts.IdAccount, codeName)
                            Else
                                codeName = dictionaryMainAccounts(accountPayableConcepts.IdAccount)
                            End If
                            apd.MainAccountNumber = codeName

                            If Not dictionaryThirdParties.ContainsKey(apd.ThirdPartyId) Then
                                codeName = _context.ThirdParty.Where(Function(d) d.Id = apd.ThirdPartyId).Select(Function(d) d.Nit + " - " + d.Name).FirstOrDefault()
                                dictionaryThirdParties.Add(apd.ThirdPartyId, codeName)
                            Else
                                codeName = dictionaryThirdParties(apd.ThirdPartyId)
                            End If
                            apd.ThirdPartyNit = codeName

                            If apd.CostCenterId IsNot Nothing Then
                                If Not dictionaryCostCenters.ContainsKey(apd.CostCenterId) Then
                                    codeName = _context.CostCenter.Where(Function(d) d.Id = apd.CostCenterId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                                    dictionaryCostCenters.Add(apd.CostCenterId, codeName)
                                Else
                                    codeName = dictionaryCostCenters(apd.CostCenterId)
                                End If
                                apd.CostCenterCode = codeName
                            End If
                            value = value + (apd.Value * If(apd.Nature = 1, 1, -1))
                        Next
                    End If
                    ap.Value = value
                Next
            End If

            Return res
        Else
            Return New LoadMassive()
        End If
    End Function

    Public Function SP_SaveLoadMassive(XmlLoadMassive As String, XmlBills As String, XmlDetails As String) As List(Of SP_SaveLoadMassive_Result) Implements ILoadMassiveRepository.SP_SaveLoadMassive
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveLoadMassive(XmlLoadMassive, XmlBills, XmlDetails).ToList()
    End Function

#End Region

End Class
