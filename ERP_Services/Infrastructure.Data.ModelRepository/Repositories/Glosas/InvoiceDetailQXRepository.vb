'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 14-06-2013
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class InvoiceDetailQXRepository
    Inherits GenericRepository(Of GlosaInvoiceDetailQX)
    Implements IInvoiceDetailQxRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="InvoiceDetailQXRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    ''' <summary>
    ''' Funcion que retorna una lista de detalle de factura QX 
    ''' </summary>
    ''' <param name="InvoiceDetailId">el Codigo del detalle de factura</param>
    ''' <returns>lista de detallle factura QX</returns>
    Public Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String) As List(Of GlosaInvoiceDetailQX) Implements IInvoiceDetailQxRepository.ListGlosaInvoiceDetailQX
        Dim Busqueda = (From e In _context.GlosaInvoiceDetailQX
             Select e
                      Where e.InvoiceDetailId = InvoiceDetailId).ToList
        Return Busqueda.ToList()
    End Function


    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String) As GlosaInvoiceDetailQX Implements IInvoiceDetailQxRepository.GetGlosaInvoiceDetailQX
        Dim Busqueda = From e In _context.GlosaInvoiceDetailQX.Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.ConceptGlosas")
                       Where e.Id = InvoiceDetailQXId
                       Select e

        If Busqueda.Count > 0 Then
            Dim itemQx = TryCast(Busqueda.SingleOrDefault, GlosaInvoiceDetailQX)
            itemQx.OriginalValue = (From e In _context.GlosaInvoiceDetailQX.AsNoTracking.Include("GlosaMovementGlosa").AsNoTracking.Include("GlosaMovementGlosa.ConceptGlosas").AsNoTracking
                                             Where e.Id = InvoiceDetailQXId
                                             Select e).SingleOrDefault
            If itemQx IsNot Nothing AndAlso itemQx.GlosaMovementGlosa IsNot Nothing Then
                itemQx.MovimientoAux = New GlosaMovementGlosa()
                itemQx.ServiceCodeName = itemQx.ServiceCode + " - " + itemQx.ServiceName
                itemQx.ServiceAreaCodeName = itemQx.ServiceAreaCode + " - " + itemQx.DescriptionServiceArea
                If itemQx.MedicalCode <> String.Empty And itemQx.MedicalName <> String.Empty Then
                    itemQx.MedicalCodeName = itemQx.MedicalCode + " - " + itemQx.MedicalName
                End If
                itemQx.CostCenterCodeName = itemQx.CostCenterCode + " - " + itemQx.CostCenterName

                Parallel.ForEach(itemQx.GlosaMovementGlosa.ToList(), Sub(itemmov As GlosaMovementGlosa)
                                                                         itemmov.ConceptGlosas.ConceptCodeName = itemmov.ConceptGlosas.Code & " - " & itemmov.ConceptGlosas.NameSpecific

                                                                         If itemmov.InvoiceDetailId = itemQx.InvoiceDetailId And itemmov.InvoiceDetailIdQX = itemQx.Id And itemmov.MainGlosa = True Then
                                                                             itemQx.ValueGlosadoFacade = itemmov.ValueGlosado
                                                                             If itemmov.ValueReiterated IsNot Nothing Then
                                                                                 itemQx.ValueReiteratedFacade = itemmov.ValueReiterated
                                                                             End If
                                                                         End If

                                                                     End Sub)
            End If

            Return itemQx
        Else
            Return New GlosaInvoiceDetailQX
        End If
    End Function
End Class
