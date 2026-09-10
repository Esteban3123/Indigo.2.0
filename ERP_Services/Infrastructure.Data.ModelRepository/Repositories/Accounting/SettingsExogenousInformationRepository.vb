'************************************************************
' Assembly         : Infrastructure.Data.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-04-2017
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class SettingsExogenousInformationRepository

    Inherits GenericRepository(Of SettingsExogenousInformation)
    Implements ISettingsExogenousInformationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetSettingsExogenousInformation(Optional tracking As Boolean = True) As List(Of SettingsExogenousInformation) Implements ISettingsExogenousInformationRepository.GetSettingsExogenousInformation
        If tracking = True Then
            Dim Busqueda = (From e In _context.SettingsExogenousInformation
                            Select e).ToList()

            If Busqueda IsNot Nothing Then


                For Each ObjBusqueda As SettingsExogenousInformation In Busqueda
                    Dim AccountingAccount = (From ma In _context.MainAccounts Where ma.Id = ObjBusqueda.MainAccountId Select ma).FirstOrDefault()

                    ObjBusqueda.MainAccountCodeName = AccountingAccount.Number + " - " + AccountingAccount.Name

                    ObjBusqueda.OriginalValue = (From e In _context.SettingsExogenousInformation.AsNoTracking Where e.Id = ObjBusqueda.Id
                                                 Select e).FirstOrDefault()

                Next


            End If
            Return Busqueda
        Else
            Dim Busqueda = (From e In _context.SettingsExogenousInformation.AsNoTracking
                            Select e).ToList()

            If Busqueda IsNot Nothing Then
                Return Busqueda
            Else
                Return New List(Of SettingsExogenousInformation)

            End If
        End If
    End Function

End Class
