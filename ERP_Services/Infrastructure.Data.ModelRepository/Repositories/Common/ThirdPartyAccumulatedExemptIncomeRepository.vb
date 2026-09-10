'************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Jorge Eduardo Guerra Rojas
' Created          : 2023-03-02
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ThirdPartyAccumulatedExemptIncomeRepository
    Inherits GenericRepository(Of ThirdpartyAccumulatedExemptIncome)
    Implements IThirdPartyAccumulatedExemptIncomeRepository, Inject

    'Contexto de Ingresos exentos acumulados de terceros
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "functions"
    ''' <summary>
    ''' Funcion para obtener la información de Ingresos exentos acumulados por tercero y año
    ''' </summary>
    ''' <returns></returns>
    Public Function GetThirdpartyYear(IdThirdParty As Integer, Year As Integer, Optional tracking As Boolean = True) As ThirdpartyAccumulatedExemptIncome Implements IThirdPartyAccumulatedExemptIncomeRepository.GetThirdpartyYear

        Dim tpaei = From e In _context.ThirdpartyAccumulatedExemptIncome
                    Where e.Year = Year AndAlso e.ThirdPartyId = IdThirdParty
                    Select e
        If tpaei.Count > 0 Then
            Dim Objtpaei = Nothing
            If tracking = False Then
                Objtpaei = (From e In _context.ThirdpartyAccumulatedExemptIncome.AsNoTracking
                            Where e.Year = Year AndAlso e.ThirdPartyId = IdThirdParty
                            Select e).SingleOrDefault
            Else
                Objtpaei = tpaei.SingleOrDefault
            End If
            Return Objtpaei
        Else
            Return New ThirdpartyAccumulatedExemptIncome()
        End If

    End Function
#End Region

End Class
