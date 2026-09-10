#Region "Imports"

Imports Domain.Entities

#End Region

''' <summary>
''' Clase parcial que contiene la lógica de conciliación automática bancaria
''' </summary>
Partial Public Class BankReconciliationAutomaticAdminService

#Region "Private Methods - Conciliation Logic"

    ''' <summary>
    ''' Busca coincidencias entre extractos bancarios y documentos de tesorería
    ''' </summary>
    ''' <param name="extractList">Lista de extractos bancarios</param>
    ''' <param name="documentList">Lista de documentos de tesorería</param>
    ''' <param name="existingAssociations">Lista de asociaciones existentes (opcional)</param>
    ''' <returns>Resultado con las listas actualizadas y las asociaciones (existentes y nuevas)</returns>
    Private Async Function FindCoincidencesInternalAsync(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), Optional existingAssociations As List(Of BankReconciliationAutomaticAssociation) = Nothing) As Task(Of BankReconciliationCoincidencesResult)
        Dim result As New BankReconciliationCoincidencesResult()

        ' Trabajar directamente con las listas originales (se reemplazarán completamente en presentación)
        ' Separar documentos tipo Nota (DocumentType = 3) con ListCashReceipts
        Dim noteDocumentsWithReceipts = documentList.Where(Function(d) d.DocumentType = 3 AndAlso d.ListCashReceipts IsNot Nothing AndAlso d.ListCashReceipts.Any()).ToList()
        Dim standardDocuments = documentList.Where(Function(d) Not (d.DocumentType = 3 AndAlso d.ListCashReceipts IsNot Nothing AndAlso d.ListCashReceipts.Any())).ToList()

        ' Inicializar lista de asociaciones con las existentes (si las hay)
        Dim associations As List(Of BankReconciliationAutomaticAssociation)
        If existingAssociations IsNot Nothing Then
            associations = New List(Of BankReconciliationAutomaticAssociation)(existingAssociations)
        Else
            associations = New List(Of BankReconciliationAutomaticAssociation)()
        End If

        ' Proceso 0: Coincidencia por Código de Transacción, Valor y Naturaleza
        ProcessCoincidencesByCodeTransactions(extractList, standardDocuments, associations)

        ' Proceso 1: Coincidencia por Fecha, Valor, Naturaleza y Tipo de Documento
        ProcessFirstCoincidences(extractList, standardDocuments, associations)

        ' Proceso 2: Coincidencia por Valor, Naturaleza y Tipo de Documento
        ProcessSecondCoincidences(extractList, standardDocuments, associations)

        ' Proceso 3: Coincidencia por Naturaleza y Tipo de Documento (A y B)
        Await ProcessThirdCoincidences(extractList, standardDocuments, associations)

        ' Proceso 4: Coincidencia para Notas con Recibos de Caja
        ProcessNoteCardCoincidences(noteDocumentsWithReceipts, extractList, associations)

        ' Retornar las listas originales con los cambios aplicados
        result.Documents = documentList
        result.Extracts = extractList
        result.Associations = associations

        Return result
    End Function

    ''' <summary>
    ''' Crea una copia clonada de un extracto bancario
    ''' </summary>
    Private Function CloneExtractDetail(original As BankReconciliationAutomaticExtractDetail) As BankReconciliationAutomaticExtractDetail
        Return New BankReconciliationAutomaticExtractDetail With {
            .Id = original.Id,
            .BankReconciliationAutomaticExtractDetailId = original.BankReconciliationAutomaticExtractDetailId,
            .UploadBankStatementsDetailId = original.UploadBankStatementsDetailId,
            .DocumentDate = original.DocumentDate,
            .ConsecutiveBank = original.ConsecutiveBank,
            .TransactionCode = original.TransactionCode,
            .DescriptionTransaction = original.DescriptionTransaction,
            .DocumentType = original.DocumentType,
            .BankCheck = original.BankCheck,
            .PaymentReferenceOne = original.PaymentReferenceOne,
            .PaymentReferenceTwo = original.PaymentReferenceTwo,
            .Nature = original.Nature,
            .Reconciled = original.Reconciled,
            .CodeNoteReconciled = original.CodeNoteReconciled,
            .Value = original.Value
        }
    End Function

    ''' <summary>
    ''' Crea una copia clonada de un documento de tesorería
    ''' </summary>
    Private Function CloneDocumentDetail(original As BankReconciliationAutomaticDetail) As BankReconciliationAutomaticDetail
        Return New BankReconciliationAutomaticDetail With {
            .Id = original.Id,
            .DocumentType = original.DocumentType,
            .Nature = original.Nature,
            .Value = original.Value,
            .EntityId = original.EntityId,
            .ListCashReceipts = original.ListCashReceipts,
            .EntityCode = original.EntityCode,
            .EntityName = original.EntityName,
            .Reconciled = original.Reconciled,
            .Comments = original.Comments,
            .DocumentDate = original.DocumentDate,
            .ThirdPartyNitName = original.ThirdPartyNitName,
            .NitThirdParty = original.NitThirdParty,
            .DocumentNumber = original.DocumentNumber,
            .Observations = original.Observations,
            .ReconciledStatus = original.ReconciledStatus,
            .CreationUser = original.CreationUser,
            .ConfirmationUser = original.ConfirmationUser
        }
    End Function

    ''' <summary>
    ''' Ejecuta el proceso de coinciliación por código de transacción y número de documento
    ''' </summary>
    Private Sub ProcessCoincidencesByCodeTransactions(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        ' Filtrar extractos que no han sido conciliados
        Dim availableExtracts = extractList.Where(Function(e) Not e.Reconciled AndAlso
                                             String.IsNullOrEmpty(e.CodeNoteReconciled)).ToList()

        ' Filtrar documentos disponibles
        Dim availableDocuments = documentList.Where(Function(d) Not d.Reconciled).ToList()

        If Not availableExtracts.Any() OrElse Not availableDocuments.Any() Then
            Return
        End If

        ' Obtener todas las coincidencias posibles
        Dim allCoincidences = (From extractDetail In availableExtracts
                               From document In availableDocuments
                               Where extractDetail.TransactionCode = document.DocumentNumber And
                                extractDetail.Nature = document.Nature And
                                Math.Abs(extractDetail.Value - document.Value) <= 1
                               Select extractDetail, document,
                                 difference = Math.Abs(extractDetail.Value - document.Value)
                          ).OrderBy(Function(x) x.difference).ToList()

        ' Controlar que cada extracto y cada documento se use solo una vez
        Dim usedExtracts As New HashSet(Of BankReconciliationAutomaticExtractDetail)()
        Dim usedDocuments As New HashSet(Of String)()
        Dim selectedCoincidences As New List(Of (extractDetail As BankReconciliationAutomaticExtractDetail, document As BankReconciliationAutomaticDetail))()

        For Each coincidence In allCoincidences
            If Not usedExtracts.Contains(coincidence.extractDetail) AndAlso
           Not usedDocuments.Contains(coincidence.document.EntityCode) Then
                selectedCoincidences.Add((coincidence.extractDetail, coincidence.document))
                usedExtracts.Add(coincidence.extractDetail)
                usedDocuments.Add(coincidence.document.EntityCode)
            End If
        Next

        If selectedCoincidences.Any() Then
            ' Relacionar el código de la Nota Conciliada
            For Each match In selectedCoincidences
                match.extractDetail.CodeNoteReconciled = match.document.EntityCode
            Next

            ' Lista de extractDetail únicos 
            Dim newExtractList = selectedCoincidences.Select(Function(item) item.extractDetail).ToList()
            Dim newDocumentList = selectedCoincidences.Select(Function(item) item.document).ToList()
            ' Se procesan las coincidencias
            HandleConciliation(newExtractList, newDocumentList, associations)
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el primer proceso de coinciliación
    ''' </summary>
    Private Sub ProcessFirstCoincidences(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        ' Filtrar extractos que no han sido conciliados
        Dim availableExtracts = extractList.Where(Function(e) Not e.Reconciled AndAlso
                                             String.IsNullOrEmpty(e.CodeNoteReconciled)).ToList()

        ' Filtrar documentos disponibles
        Dim availableDocuments = documentList.Where(Function(d) Not d.Reconciled).ToList()

        If Not availableExtracts.Any() OrElse Not availableDocuments.Any() Then
            Return
        End If

        ' Obtener todas las coincidencias posibles
        Dim allCoincidences = (From extractDetail In availableExtracts
                               From document In availableDocuments
                               Where extractDetail.DocumentType = document.DocumentType And
                                extractDetail.Nature = document.Nature And
                                Math.Abs(extractDetail.Value - document.Value) <= 1 And
                                extractDetail.DocumentDate.Date = document.DocumentDate
                               Select extractDetail, document,
                                 difference = Math.Abs(extractDetail.Value - document.Value)
                          ).OrderBy(Function(x) x.difference).ToList()

        ' Controlar que cada extracto y cada documento se use solo una vez
        Dim usedExtracts As New HashSet(Of BankReconciliationAutomaticExtractDetail)()
        Dim usedDocuments As New HashSet(Of String)()
        Dim selectedCoincidences As New List(Of (extractDetail As BankReconciliationAutomaticExtractDetail, document As BankReconciliationAutomaticDetail))()

        For Each coincidence In allCoincidences
            If Not usedExtracts.Contains(coincidence.extractDetail) AndAlso
           Not usedDocuments.Contains(coincidence.document.EntityCode) Then
                selectedCoincidences.Add((coincidence.extractDetail, coincidence.document))
                usedExtracts.Add(coincidence.extractDetail)
                usedDocuments.Add(coincidence.document.EntityCode)
            End If
        Next

        If selectedCoincidences.Any() Then
            ' Relacionar el código de la Nota Conciliada
            For Each match In selectedCoincidences
                match.extractDetail.CodeNoteReconciled = match.document.EntityCode
            Next

            ' Lista de extractDetail únicos 
            Dim newExtractList = selectedCoincidences.Select(Function(item) item.extractDetail).ToList()
            Dim newDocumentList = selectedCoincidences.Select(Function(item) item.document).ToList()
            ' Se procesan las coincidencias
            HandleConciliation(newExtractList, newDocumentList, associations)
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el segundo proceso de coinciliación
    ''' Incluye filtro de fecha variable según día de la semana: Lunes/Viernes ±5 días, otros días ±3 días
    ''' </summary>
    Private Sub ProcessSecondCoincidences(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        ' Filtrar extractos que no han sido conciliados
        Dim availableExtracts = extractList.Where(Function(e) Not e.Reconciled AndAlso
                                             String.IsNullOrEmpty(e.CodeNoteReconciled)).ToList()

        ' Filtrar documentos disponibles
        Dim availableDocuments = documentList.Where(Function(d) Not d.Reconciled).ToList()

        If Not availableExtracts.Any() OrElse Not availableDocuments.Any() Then
            Return
        End If

        ' Obtener todas las coincidencias posibles con filtro de fecha variable según día de la semana
        Dim allCoincidences = (From extractDetail In availableExtracts
                               From document In availableDocuments
                               Let docDay = document.DocumentDate.DayOfWeek
                               Let startDate = document.DocumentDate.AddDays(-1).Date
                               Let endDate = If(docDay = DayOfWeek.Monday OrElse docDay = DayOfWeek.Friday,
                                                document.DocumentDate.AddDays(3).Date,
                                                document.DocumentDate.AddDays(1).Date)
                               Where extractDetail.DocumentType = document.DocumentType And
                                extractDetail.Nature = document.Nature And
                                Math.Abs(extractDetail.Value - document.Value) <= 1 And
                                extractDetail.DocumentDate.Date >= startDate And
                                extractDetail.DocumentDate.Date <= endDate
                               Select extractDetail, document,
                                 difference = Math.Abs(extractDetail.Value - document.Value)
                          ).OrderBy(Function(x) x.difference).ToList()

        ' Controlar que cada extracto y cada documento se use solo una vez
        Dim usedExtracts As New HashSet(Of BankReconciliationAutomaticExtractDetail)()
        Dim usedDocuments As New HashSet(Of String)()
        Dim selectedCoincidences As New List(Of (extractDetail As BankReconciliationAutomaticExtractDetail, document As BankReconciliationAutomaticDetail))()

        For Each coincidence In allCoincidences
            If Not usedExtracts.Contains(coincidence.extractDetail) AndAlso
           Not usedDocuments.Contains(coincidence.document.EntityCode) Then
                selectedCoincidences.Add((coincidence.extractDetail, coincidence.document))
                usedExtracts.Add(coincidence.extractDetail)
                usedDocuments.Add(coincidence.document.EntityCode)
            End If
        Next

        If selectedCoincidences.Any() Then
            ' Relacionar el código de la Nota Conciliada
            For Each match In selectedCoincidences
                match.extractDetail.CodeNoteReconciled = match.document.EntityCode
            Next

            ' Lista de extractDetail únicos 
            Dim newExtractList = selectedCoincidences.Select(Function(item) item.extractDetail).ToList()
            Dim newDocumentList = selectedCoincidences.Select(Function(item) item.document).ToList()
            ' Se procesan las coincidencias
            HandleConciliation(newExtractList, newDocumentList, associations)
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el tercer proceso de Conciliación
    ''' </summary>
    Private Async Function ProcessThirdCoincidences(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation)) As Task
        Dim coincidences = (From extractDetail In extractList
                            Join document In documentList
                            On extractDetail.DocumentType Equals document.DocumentType And
                            extractDetail.Nature Equals document.Nature
                            Where Not document.Reconciled
                            Select extractDetail, document).Distinct().ToList()

        If coincidences.Any() Then

            Dim groupedCoincidences = coincidences.GroupBy(Function(x) x.extractDetail.DocumentType).ToList()

            For Each group In groupedCoincidences
                Dim newExtractList = group.Select(Function(item) item.extractDetail).Distinct().ToList()
                Dim newDocumentList = group.Select(Function(item) item.document).Distinct().ToList()

                Await ThirdReconciliationProcessA(newExtractList, newDocumentList, associations)
            Next

            ' Verificar si quedan extractos y ejecutar ThirdReconciliationProcessB
            If extractList.Any(Function(e) Not e.Reconciled) Then
                Dim remainingCoincidences = (From extractDetail In extractList
                                             Join document In documentList
                                             On extractDetail.DocumentType Equals document.DocumentType And
                                             extractDetail.Nature Equals document.Nature
                                             Where Not document.Reconciled
                                             Select extractDetail, document).Distinct().ToList()

                If remainingCoincidences.Any() Then

                    Dim groupedRemainingCoincidences = remainingCoincidences.GroupBy(Function(x) x.extractDetail.DocumentType).ToList()

                    For Each group In groupedRemainingCoincidences
                        Dim remainingExtractList = group.Select(Function(item) item.extractDetail).Distinct().ToList()
                        Dim remainingDocumentList = group.Select(Function(item) item.document).Distinct().ToList()

                        ThirdReconciliationProcessB(remainingExtractList, remainingDocumentList, associations)
                    Next
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Tercera función o proceso para coinciliar
    ''' Buscando detalles del extracto que coincidan con el valor de una nota
    ''' </summary>
    Private Async Function ThirdReconciliationProcessA(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation)) As Task
        Dim tolerance As Decimal = 1

        For Each document In documentList
            If Not document.Reconciled Then
                Dim requiredSum As Decimal = document.Value
                Dim targetMin As Decimal = requiredSum - tolerance
                Dim targetMax As Decimal = requiredSum + tolerance

                Dim day = document.DocumentDate.DayOfWeek
                Dim extractsToSearch As List(Of BankReconciliationAutomaticExtractDetail)
                'Filtramos datos por la fecha
                If day = DayOfWeek.Monday OrElse day = DayOfWeek.Friday Then
                    Dim startDate = document.DocumentDate.AddDays(-1).Date
                    Dim endDate = document.DocumentDate.AddDays(3).Date
                    extractsToSearch = extractList.
                        Where(Function(e) Not e.Reconciled AndAlso e.DocumentDate.Date >= startDate AndAlso e.DocumentDate.Date <= endDate).
                        OrderBy(Function(e) e.Value).
                        ToList()
                Else
                    Dim startDate = document.DocumentDate.AddDays(-1).Date
                    Dim endDate = document.DocumentDate.AddDays(1).Date
                    extractsToSearch = extractList.
                        Where(Function(e) Not e.Reconciled AndAlso e.DocumentDate.Date >= startDate AndAlso e.DocumentDate.Date <= endDate).
                        OrderBy(Function(e) e.Value).
                        ToList()
                End If

                Dim reconciledExtracts As List(Of BankReconciliationAutomaticExtractDetail) = FindSubsetsWithinToleranceA(extractsToSearch, targetMin, targetMax)
                If reconciledExtracts.Any() Then
                    HandleConciliationOneToMany(document, reconciledExtracts, associations)
                End If
            End If
        Next
    End Function

    ''' <summary>
    ''' Tercera función o proceso para coinciliar
    ''' Buscando valores de notas que sumadas den un valor del detalle del extracto
    ''' </summary>
    Private Sub ThirdReconciliationProcessB(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        Dim tolerance As Decimal = 1

        For Each extract In extractList
            If Not extract.Reconciled Then
                Dim requiredSum As Decimal = extract.Value
                Dim targetMin As Decimal = requiredSum - tolerance
                Dim targetMax As Decimal = requiredSum + tolerance

                Dim day = extract.DocumentDate.DayOfWeek
                Dim documentsToSearch As List(Of BankReconciliationAutomaticDetail)
                'Filtramos datos por la fecha
                If day = DayOfWeek.Monday OrElse day = DayOfWeek.Friday Then
                    Dim startDate = extract.DocumentDate.AddDays(-1).Date
                    Dim endDate = extract.DocumentDate.AddDays(3).Date
                    documentsToSearch = documentList.
                        Where(Function(d) Not d.Reconciled AndAlso d.DocumentDate >= startDate AndAlso d.DocumentDate <= endDate).
                        OrderBy(Function(d) d.Value).
                        ToList()
                Else
                    Dim startDate = extract.DocumentDate.AddDays(-1).Date
                    Dim endDate = extract.DocumentDate.AddDays(1).Date
                    documentsToSearch = documentList.
                        Where(Function(d) Not d.Reconciled AndAlso d.DocumentDate >= startDate AndAlso d.DocumentDate <= endDate).
                        OrderBy(Function(d) d.Value).
                        ToList()
                End If

                Dim documentsReconciled As List(Of BankReconciliationAutomaticDetail) = FindSubsetsWithinToleranceB(documentsToSearch, targetMin, targetMax)

                If documentsReconciled.Any() Then
                    HandleConciliationManyToOne(documentsReconciled, extract, associations)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Procesa coincidencias para documentos tipo Nota (DocumentType = 3) con ListCashReceipts (Nota de Comisiones a Tarjetas)
    ''' </summary>
    Private Sub ProcessNoteCardCoincidences(noteDocuments As List(Of BankReconciliationAutomaticDetail), extractList As List(Of BankReconciliationAutomaticExtractDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        ' Filtrar extractos no conciliados
        Dim availableExtracts = extractList.Where(Function(e) Not e.Reconciled AndAlso String.IsNullOrEmpty(e.CodeNoteReconciled)).ToList()

        If Not availableExtracts.Any() OrElse Not noteDocuments.Any() Then Return

        For Each document In noteDocuments
            ' Obtener valor real y naturaleza
            Dim realValueAndNature As Tuple(Of Decimal, Byte) = CalculateRealValueAndNature(document)

            Dim realValue As Decimal = realValueAndNature.Item1
            Dim realNature As Byte = realValueAndNature.Item2

            ' Encontrar coincidencias (deben ser recibos de caja)
            Dim tolerance As Decimal = 1
            Dim matchingExtracts = availableExtracts.Where(Function(e) e.Nature = realNature AndAlso
                Math.Abs(e.Value - realValue) <= tolerance AndAlso
                e.DocumentType = 1
            ).ToList()

            If Not matchingExtracts.Any() Then Continue For

            ' Si hay varias opciones encontrar el de la fecha más cercana
            Dim selectedExtract As BankReconciliationAutomaticExtractDetail

            If matchingExtracts.Count = 1 Then
                selectedExtract = matchingExtracts(0)
            Else
                selectedExtract = matchingExtracts.OrderBy(Function(e) Math.Abs((e.DocumentDate - document.DocumentDate).TotalDays)).First()
            End If

            ' Realizar conciliación
            selectedExtract.CodeNoteReconciled = document.EntityCode
            HandleConciliation(New List(Of BankReconciliationAutomaticExtractDetail) From {selectedExtract},
                                     New List(Of BankReconciliationAutomaticDetail) From {document}, associations)

            ' Eliminar de los extractos disponibles
            availableExtracts.Remove(selectedExtract)
        Next
    End Sub

    ''' <summary>
    ''' Agrega coincidencias marcando como conciliados los detalles del extracto y documentos que tienen coincidencias
    ''' </summary>
    Private Sub HandleConciliation(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        'Marcar como Conciliados los detalles del extracto que tienen coincidencias
        extractList.ForEach(Sub(extract)
                                ' Verificar que el extracto no haya sido conciliado previamente
                                If Not extract.Reconciled AndAlso Not String.IsNullOrEmpty(extract.CodeNoteReconciled) Then
                                    'Marcar los documentos coinciliados
                                    For Each document In documentList
                                        If document.EntityCode = extract.CodeNoteReconciled Then
                                            If Not document.Reconciled Then 'Validamos que se realice conciliación con Documentos que no hayan sido conciliados
                                                extract.Reconciled = True
                                                document.Reconciled = True
                                                CreateAutomaticAssociation(document, extract, associations)
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End Sub)
    End Sub

    ''' <summary>
    ''' Realiza conciliación manual entre extractos y documentos seleccionados
    ''' </summary>
    Private Sub HandleConciliationOneToOne(extractDetails As List(Of BankReconciliationAutomaticExtractDetail), documentDetails As List(Of BankReconciliationAutomaticDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        ' Asignar códigos de conciliación antes de llamar a HandleConciliation
        For Each document In documentDetails
            For Each extractDetail In extractDetails
                ' Relacionar el código de la Nota Conciliada
                extractDetail.CodeNoteReconciled = document.EntityCode
            Next
        Next
        ' Procesar la conciliación 
        HandleConciliation(extractDetails, documentDetails, associations)
    End Sub

    ''' <summary>
    ''' Realiza el proceso de conciliación de un documento a muchos extractos
    ''' </summary>
    Private Sub HandleConciliationOneToMany(document As BankReconciliationAutomaticDetail, reconciledExtracts As List(Of BankReconciliationAutomaticExtractDetail), associations As List(Of BankReconciliationAutomaticAssociation))
        For Each extractReconciled In reconciledExtracts
            extractReconciled.Reconciled = True
            extractReconciled.CodeNoteReconciled = document.EntityCode
            CreateAutomaticAssociation(document, extractReconciled, associations)
        Next

        ' Marcar documento como conciliado
        document.Reconciled = True
    End Sub

    ''' <summary>
    ''' Realiza el proceso de conciliación de muchos documentos a un detalle del extracto
    ''' </summary>
    Private Sub HandleConciliationManyToOne(documentsReconciled As List(Of BankReconciliationAutomaticDetail), extract As BankReconciliationAutomaticExtractDetail, associations As List(Of BankReconciliationAutomaticAssociation))
        ' Concatenar EntityCode de todas las notesReconciled
        Dim concatenatedEntityCodes As String = String.Join(", ", documentsReconciled.Select(Function(n) n.EntityCode))
        ' Se marca el detalle del extracto como conciliado
        extract.Reconciled = True
        extract.CodeNoteReconciled = concatenatedEntityCodes 'Se relaciona el código de las notas que fueron coinciliadas

        For Each documentReconciled In documentsReconciled
            ' Marcar documentos como conciliados
            documentReconciled.Reconciled = True
            CreateAutomaticAssociation(documentReconciled, extract, associations)
        Next
    End Sub

    ''' <summary>
    ''' Maneja la conciliación de muchos documentos contra muchos extractos (N:M).
    ''' </summary>
    Private Sub HandleConciliationManyToMany(documents As List(Of BankReconciliationAutomaticDetail), extracts As List(Of BankReconciliationAutomaticExtractDetail), associations As List(Of BankReconciliationAutomaticAssociation))

        If documents Is Nothing OrElse documents.Count = 0 Then Exit Sub
        If extracts Is Nothing OrElse extracts.Count = 0 Then Exit Sub

        ' 1) Preparar concatenación de códigos de documentos (se usará en los extractos)
        Dim concatenatedEntityCodes As String = String.Join(", ", documents.
                                                        Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(d.EntityCode)).
                                                        Select(Function(d) d.EntityCode).
                                                        Distinct())

        ' 2) Marcar extractos y asignar CodeNoteReconciled
        For Each ex In extracts
            If ex Is Nothing Then Continue For
            ex.Reconciled = True
            ex.CodeNoteReconciled = concatenatedEntityCodes
        Next

        ' 3) Marcar documentos como conciliados
        For Each doc In documents
            If doc Is Nothing Then Continue For
            doc.Reconciled = True
        Next

        ' 4) Crear asociaciones N:M (evitar duplicados si ya existen)
        For Each doc In documents
            If doc Is Nothing Then Continue For
            For Each ex In extracts
                If ex Is Nothing Then Continue For

                ' Validar duplicados: usar ID si ambos están persistidos, sino usar referencia
                Dim exists As Boolean = False

                If doc.Id > 0 AndAlso ex.Id > 0 Then
                    ' Ambos objetos están persistidos, usar ID para comparación
                    exists = associations.Any(Function(a) _
                        a IsNot Nothing AndAlso
                        a.BankReconciliationAutomaticDetail IsNot Nothing AndAlso
                        a.BankReconciliationAutomaticExtractDetail IsNot Nothing AndAlso
                        a.BankReconciliationAutomaticDetail.Id = doc.Id AndAlso
                        a.BankReconciliationAutomaticExtractDetail.Id = ex.Id)
                Else
                    ' Al menos uno es nuevo (Id = 0), usar referencia de objeto
                    exists = associations.Any(Function(a) _
                        a IsNot Nothing AndAlso
                        Object.ReferenceEquals(a.BankReconciliationAutomaticDetail, doc) AndAlso
                        Object.ReferenceEquals(a.BankReconciliationAutomaticExtractDetail, ex))
                End If

                If Not exists Then
                    Dim assoc As New BankReconciliationAutomaticAssociation With {
                    .BankReconciliationAutomaticDetail = doc,
                    .BankReconciliationAutomaticExtractDetail = ex
                }
                    associations.Add(assoc)
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Crea una asociación automática entre un documento y un extracto
    ''' Valida duplicados usando ID para objetos persistidos o referencia para objetos nuevos
    ''' </summary>
    Private Sub CreateAutomaticAssociation(document As BankReconciliationAutomaticDetail, extractDetail As BankReconciliationAutomaticExtractDetail, associations As List(Of BankReconciliationAutomaticAssociation))
        ' Validar si ya existe una asociación con el mismo documento y extracto
        ' Usar ID si ambos objetos están persistidos (Id > 0), sino usar referencia de objeto
        Dim exists As Boolean = False

        If document.Id > 0 AndAlso extractDetail.Id > 0 Then
            ' Ambos objetos están persistidos, usar ID para comparación
            exists = associations.Any(Function(a) _
                a IsNot Nothing AndAlso
                a.BankReconciliationAutomaticDetail IsNot Nothing AndAlso
                a.BankReconciliationAutomaticExtractDetail IsNot Nothing AndAlso
                a.BankReconciliationAutomaticDetail.Id = document.Id AndAlso
                a.BankReconciliationAutomaticExtractDetail.Id = extractDetail.Id)
        Else
            ' Al menos uno es nuevo (Id = 0), usar referencia de objeto
            exists = associations.Any(Function(a) _
                a IsNot Nothing AndAlso
                Object.ReferenceEquals(a.BankReconciliationAutomaticDetail, document) AndAlso
                Object.ReferenceEquals(a.BankReconciliationAutomaticExtractDetail, extractDetail))
        End If

        If Not exists Then
            Dim automaticAssociation As New BankReconciliationAutomaticAssociation
            With automaticAssociation
                .BankReconciliationAutomaticDetail = document
                .BankReconciliationAutomaticExtractDetail = extractDetail
            End With
            associations.Add(automaticAssociation)
        End If
    End Sub

#End Region

End Class

