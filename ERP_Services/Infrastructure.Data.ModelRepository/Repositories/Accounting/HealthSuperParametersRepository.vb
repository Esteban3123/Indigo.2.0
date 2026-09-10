#Region "Importar"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class HealthSuperParametersRepository

    Inherits GenericRepository(Of HealthSuperParameters)
    Implements IHealthSuperParametersRepository

    'Devuelve el contexto en este repositorio
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un registro por Codidgo
    ''' </summary>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetHealthSuperParametersByCode(Code As String) As HealthSuperParameters Implements IHealthSuperParametersRepository.GetHealthSuperParametersByCode
        If Code Is Nothing OrElse Code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If

        Dim res = (From d As HealthSuperParameters In _context.HealthSuperParameters.Include("HealthSuperParametersFt004").Include("HealthSuperParametersFt004.HealthSuperParametersFt004Detail").Include("HealthSuperParametersFt006").Include("HealthSuperParametersFt007").Include("HealthSuperParametersFt008").Include("HealthSuperParametersFt003").Include("HealthSuperParametersFt009")
                   Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then
            If res.HealthSuperParametersFt004 IsNot Nothing AndAlso res.HealthSuperParametersFt004.Count > 0 Then
                For Each detail In res.HealthSuperParametersFt004

                    Dim MainAccount = MainAccountFunction(detail.MainAccountId)
                    detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name

                    For Each item In detail.HealthSuperParametersFt004Detail

                        Dim JournalVoucher = (From j In _context.JournalVouchers.AsNoTracking.Include("JournalVoucherTypes").Include("JournalVoucherDetails") Where j.Id = item.JournalVouchersId).FirstOrDefault
                        item.Consecutive = JournalVoucher.Consecutive
                        item.JournalVoucherTypeName = JournalVoucher.JournalVoucherTypes.Name
                        item.Detail = JournalVoucher.Detail

                        Select Case JournalVoucher.Status
                            Case 1
                                item.StatusName = "Registrado"
                            Case 2
                                item.StatusName = "Confirmado"
                            Case Else
                                item.StatusName = "Anulado"
                        End Select

                    Next
                Next
            ElseIf res.HealthSuperParametersFt006 IsNot Nothing AndAlso res.HealthSuperParametersFt006.Count > 0 Then

                For Each Detail In res.HealthSuperParametersFt006
                    Dim MainAccount = MainAccountFunction(Detail.MainAccountId)
                    Dim ThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = Detail.ThirdPartyId Select a).FirstOrDefault()
                    Detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name
                    Detail.ThirdPartyNitName = ThirdParty.Nit & " - " & ThirdParty.Name
                    Detail.RatingEntityName = RatingEntityName(Detail.RatingEntity)
                    Detail.ClassAccountName = ClassAccountName(Detail.ClassAccount)
                    Detail.StatusName = StatusName(Detail.Status)
                    IIf(Detail.MeasureDate = #1/01/0001#, Detail.MeasureDateName = "00000000", Detail.MeasureDateName = Detail.MeasureDate.ToString)

                Next
            ElseIf res.HealthSuperParametersFt007 IsNot Nothing AndAlso res.HealthSuperParametersFt007.Count > 0 Then
                For Each Detail In res.HealthSuperParametersFt007

                    Dim MainAccount = MainAccountFunction(Detail.MainAccountId)
                    Dim ThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = Detail.ThirdPartyId Select a).FirstOrDefault()
                    Detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name
                    Detail.ThirdPartyNitName = ThirdParty.Nit & " - " & ThirdParty.Name
                    Detail.RatingEntityName = RatingEntityName(Detail.RatingEntity)
                    Detail.StatusName = StatusName(Detail.Status)
                    Detail.InstrumentName = InstrumentName(Detail.Instrument)
                    Detail.InvestmentTypeName = InvestmentTypeName(Detail.InvestmentType)
                    Detail.ModalityName = ModalityName(Detail.Modality)
                    Detail.PeriodicityName = PeriodicityName(Detail.Periodicity)
                    Detail.DematerializedName = DematerializedName(Detail.Dematerialized)
                    IIf(Detail.MeasureDate Is Nothing, Detail.MeasureDateName = "00000000", Detail.MeasureDateName = Detail.MeasureDate.ToString())
                    IIf(Detail.InvestmentTechnicalReserves, Detail.InvestmentTechnicalReservesName = " Si Respalda las Reservas Técnicas", Detail.InvestmentTechnicalReservesName = "No aplica para SAP, EMP e IPS o no es inversión que respalde las Reservas Técnicas")
                Next

            ElseIf res.HealthSuperParametersFt008 IsNot Nothing AndAlso res.HealthSuperParametersFt008.Count > 0 Then

                For Each Detail In res.HealthSuperParametersFt008

                    Dim MainAccount = MainAccountFunction(Detail.MainAccountId)
                    Dim ThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = Detail.ThirdPartyId Select a).FirstOrDefault()
                    Detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name
                    Detail.ThirdPartyNitName = ThirdParty.Nit & " - " & ThirdParty.Name
                    Detail.StatusName = StatusName(Detail.Status)
                    Detail.InvestmentTypeName = InvestmentTypeName(Detail.InvestmentType)
                    Detail.ModalityName = ModalityName(Detail.Modality)
                    Detail.PeriodicityName = PeriodicityName(Detail.Periodicity)
                    IIf(Detail.MeasureDate Is Nothing, Detail.MeasureDateName = "00000000", Detail.MeasureDateName = Detail.MeasureDate.ToString())
                Next

            ElseIf res.HealthSuperParametersFt003 IsNot Nothing AndAlso res.HealthSuperParametersFt003.Count > 0 Then
                For Each Detail In res.HealthSuperParametersFt003
                    Dim MainAccount = MainAccountFunction(Detail.MainAccountId)
                    Detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name
                Next

            ElseIf res.HealthSuperParametersFt009 IsNot Nothing AndAlso res.HealthSuperParametersFt009.Count > 0 Then
                For Each Detail In res.HealthSuperParametersFt009
                    Dim MainAccount = MainAccountFunction(Detail.MainAccountId)
                    Detail.MainAccountNumberName = MainAccount.Number & " - " & MainAccount.Name
                Next
            End If

        res.OriginalValue = (From d As HealthSuperParameters In Me._context.HealthSuperParameters.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New HealthSuperParameters()
        End If
    End Function

    Private Function RatingEntityName(RatingEntity As Byte) As String
        Select Case RatingEntity
            Case 0
                Return "No Aplica"
            Case 1
                Return "BRC Investor Services S.A. (Stand & Poors)"
            Case 2
                Return "Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.)"
            Case 3
                Return "Value And Risk Rating S.A."
            Case 4
                Return "Otra Sociedad Calificadora"
            Case Else
                Return "N/A"
        End Select
    End Function

    Private Function ClassAccountName(ClassAccount As Byte) As String
        Select Case ClassAccount
            Case 1
                Return "Cuenta Corriente"
            Case 2
                Return "Cuenta de Ahorros"
            Case 3
                Return "Cuenta Maestra de Recaudo"
            Case 4
                Return "Cartera Colectiva Abierta o Fondos de Inversión en Mercado Monetario"
            Case 5
                Return "Cartera Colectiva Cerrada"
            Case 6
                Return "Otro tipo de Encargo Fiduciario o Fondo de Inversión, Fideicomiso, Fondos de Inversión Colectiva Inmobiliarios y/o Fondos de Capital Privado"
            Case Else
                Return "N/A"
        End Select
    End Function

    Private Function StatusName(Status As Byte) As String
        Select Case Status
            Case 0
                Return "No Aplica"
            Case 1
                Return "Libre de afectación"
            Case 2
                Return "Embargos"
            Case 3
                Return "Medida Preventiva"
            Case Else
                Return "N/A"
        End Select
    End Function

    Private Function InstrumentName(Instrument As Byte) As String
        Select Case Instrument
            Case 1
                Return "Títulos de deuda pública emitidos o garantizados por la Nación o por el Banco de la República"
            Case 2
                Return "Títulos de renta fija emitidos, aceptados, garantizados o avalados por entidades vigiladas por la Superintendencia Financiera de Colombia, FOGAFIN y FOGACOOP."
            Case 3
                Return "Renta Variable."
            Case Else
                Return "N/A"
        End Select
    End Function

    Private Function InvestmentTypeName(InvestmentType As Byte) As String
        Select Case InvestmentType
            Case 1
                Return "Títulos de Deuda Pública (TES)"
            Case 2
                Return "Certificados de Depósito a Término (CDT)"
            Case 3
                Return "Bonos Ordinarios"
            Case 4
                Return "Bonos Subordinados"
            Case 5
                Return "Bonos Opcionalmente Convertibles en acciones"
            Case 6
                Return "Bonos Obligatoriamente Convertibles en acciones"
            Case 7
                Return "Bonos de Capitalización"
            Case 8
                Return "Acciones Ordinarias"
            Case 9
                Return "Acciones preferenciales"
            Case Else
                Return "Otro"
        End Select
    End Function

    Private Function ModalityName(Modality As Byte) As String
        Select Case Modality
            Case 0
                Return "No Aplica."
            Case 1
                Return "Vencida"
            Case 2
                Return "Anticipada"
            Case Else
                Return "N/A"
        End Select
    End Function

    Private Function PeriodicityName(Periodicity As String) As String
        Select Case Periodicity
            Case "M"
                Return "Mensual"
            Case "B"
                Return "Bimestral"
            Case "T"
                Return "Trimestral"
            Case "S"
                Return "Semestral"
            Case "A"
                Return "Anual"
            Case Else
                Return "Otro o No Aplica"
        End Select
    End Function

    Private Function DematerializedName(Dematerialized As Byte) As String
        Select Case Dematerialized
            Case 1
                Return "DECEVAL S.A. - Depósito Centralizado de Valores."
            Case 2
                Return " DCV - Depósito Central de Valores."
            Case Else
                Return "N/A"
        End Select
    End Function

    ''' <summary>
    ''' Consulta la tabla MainAccount
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    Private Function MainAccountFunction(MainAccountId As Integer) As MainAccounts
        Dim MainAccount = (From a In _context.MainAccounts.AsNoTracking Where a.Id = MainAccountId Select a).FirstOrDefault()
        Return MainAccount
    End Function


    ''' <summary>
    ''' Obtiene una cuenta por pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetHealthSuperParametersById(Id As Integer) As HealthSuperParameters Implements IHealthSuperParametersRepository.GetHealthSuperParametersById
        Dim HealthSuperParameters = (From e In _context.HealthSuperParameters Where e.Id = Id Select e).FirstOrDefault
        Dim res = GetHealthSuperParametersByCode(HealthSuperParameters.Code)
        If res IsNot Nothing Then
            Return res
        Else
            Return New HealthSuperParameters()
        End If
    End Function

    Public Function GetHealthSuperParametersAll() As List(Of HealthSuperParameters) Implements IHealthSuperParametersRepository.GetHealthSuperParametersAll
        Dim ListHealthSuperParameters = (From e In _context.HealthSuperParameters.AsNoTracking()).ToList()
        If ListHealthSuperParameters IsNot Nothing Then
            Return ListHealthSuperParameters
        Else
            Return New List(Of HealthSuperParameters)
        End If
    End Function

End Class