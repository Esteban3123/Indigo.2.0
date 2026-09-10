'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Julian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 11-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
#End Region
<ServiceContract()> _
Public Interface IGlosasService
    Inherits IGlosasConciliationC, IGlosasCustomer, IGlosaMovementGlosa
    Inherits IGlosasObjectionsReceptionC, IGlosasResponsible, IGlosasTransferJuridicalDebtD
    Inherits IGlosasInvoiceDetail, IGlosasConciliationParticipants, IGlosasConciliationD, IGlosasMovementDevolutions
    Inherits IGlosasPortfolioGlosada, IGlosasDevolutionsReceptionC, IGlosasDevolutionsReceptionD
    Inherits IGlosasObjectionsReceptionD, IGlosasJustificationTemplate, IGlosasTransferJuridicalDebtC
    Inherits IGlosasTimeParameters, IGlosasInterfaceParameters, IGlosasRadicateInvoiceC, IGlosasRadicateInvoiceD, IGlosasPartialPayments, IGlosasRIPSPlane
    Inherits IGlosasResponseHierarchy, IGlosasGlosasMassiveConfirm, IGlosasReports, IGlosasImportunityCauses
    Inherits IGlosasSequence, IGlosasConceptGlosas
End Interface
