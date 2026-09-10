'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class TrademarkRepository

    Inherits GenericRepository(Of Trademark)
    Implements ITrademarkRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetTrademarkByCode(Code As String, Optional tracking As Boolean = True) As Trademark Implements ITrademarkRepository.GetTrademarkByCode
       
        Dim Trademark = From e In _context.Trademark
                        Where e.Code = Code
                        Select e
        If Trademark.Count > 0 Then
            Dim objTrademark = Nothing
            If tracking = False Then
                objTrademark = (From e In _context.Trademark.AsNoTracking
                                 Where e.Code = Code
                                 Select e).SingleOrDefault
            Else
                objTrademark = Trademark.SingleOrDefault()
            End If
            Return CType(objTrademark, Domain.Maintenance.Entities.Trademark)
        Else
            Return New Trademark()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTrademark() As List(Of Trademark) Implements ITrademarkRepository.ListAllTrademark
        Dim ListTrademark = From e In _context.Trademark
                   Select e

        If ListTrademark.Count() > 0 Then
            Return ListTrademark.ToList()
        Else
            Return New List(Of Trademark)
        End If


    End Function
End Class
