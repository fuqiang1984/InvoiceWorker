<details open="open">
  <summary>Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#overall-design">Overall design</a>
    </li>
    <li><a href="#mock-endpoint">Mock endpoint</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

## About the project
This is a POC to poll events from a mock endpoint.And create, update or delete invoice according to the request.



## Overall design
Working process:

-> Invoice worker will constantly polling the latest event json and deserialize it to be event detail object via eventservice.
-> Invoice will send each invoice to Event Processor.
-> Invoice processor will process the invoice according to the type of request (INVOICE_CREATED,INVOICE_UPDATED,INVOICE_DELETED)
-> The document service will create, update or delete invoice.



InvoiceWorker: 
1. It is a startup project with worker service constantly polling the latest event via EventService.
2. A txt file called LatestEventID to store the latest event id.It is used to make sure to process the event feed from the previous position it left off.
3. InvoiceWorker will trigger EventDetail processor to create, update or delete pdf.

InvoiceWorker.EventDetailProcessors
1. Strategy pattern to make sure it can be resilient and easy to extend.
e.g. When some new type of action is required, just add one more processor to handle the new type.
2. Handle different types of request (INVOICE_CREATED,INVOICE_UPDATED,INVOICE_DELETED)


InvoiceWorker.DataSerializer:
1. It is used to deserialise json response to be an event detail object.


Invoice.Document.Services:
1. It is used to create, update and delete pdf files.
2. PdfSharpCore 1.2.6 is installed as the package to handle the creation of pdf.

InvoiceWorker.Events.Integration.Services:
1. Connect to events endpoints to get back the event json response and translate it to be object of event detail.

InvoiceWorker.Event.Models:
1. Definition of event detail models.


## Mock endpoint
1.For the get request 'GET /invoices/events?pageSize=1&afterEventId=0', 

2. For the get request 'GET /invoices/events?pageSize=1&afterEventId=0'

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/fuqiang1984/InvoiceWorker/issues) for a list of proposed features (and known issues).

!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Qiang Fu john.fu1984@example.com

Project Link: [https://github.com/fuqiang1984/InvoiceWorker](https://github.com/fuqiang1984/InvoiceWorker)








