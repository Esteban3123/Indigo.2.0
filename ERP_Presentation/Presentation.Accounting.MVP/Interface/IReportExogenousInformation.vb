Imports DevExpress.Xpo

Public Interface IReportExogenousInformation

#Region "Properties"

    ReadOnly Property Year As Integer?

    ReadOnly Property ExogenousFormatId As Integer?

    ReadOnly Property Format As String

    ReadOnly Property Version As Integer?

    ReadOnly Property Concept As Integer?

    ReadOnly Property SendingNumber As Integer?

#End Region

#Region "Datasources"

    Property FormatXpo As XPCollection(Of Infrastructure.Data.Xpo.AccountingRepository.ExogenousFormatXpo)

#End Region

End Interface
