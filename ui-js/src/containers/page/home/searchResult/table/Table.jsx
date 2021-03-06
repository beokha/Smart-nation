import './table.scss';
import {NoResult} from './NoResult/NoResult';

import React from 'react';
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import {Link} from 'react-router-dom';

const Table = ({className, data, columns}) => {
    data.map(book => addOrderButtonToEachBook(book));
    return (
        <div className={className}>
            <BootstrapTable data={data} striped={true} hover={true} version='4'>
                <TableHeaderColumn dataField="id" isKey={true} dataAlign="center" dataSort={true}>
                    ID
                </TableHeaderColumn>
                <TableHeaderColumn dataField="title" dataSort={true}>
                    Title
                </TableHeaderColumn>
                <TableHeaderColumn dataField="author" dataSort={true}>
                    Author
                </TableHeaderColumn>
                <TableHeaderColumn dataField="createdOn" dataSort={true}>
                    Added to system
                </TableHeaderColumn>
                <TableHeaderColumn  
                    dataField="orderBook"
                    dataSort={false}
                    dataFormat={showOrderButton}>
                    Book page
                </TableHeaderColumn>
            </BootstrapTable>
        </div>
    )
}

const addOrderButtonToEachBook = book => 
    book.orderBook = {
        order: <Link to={`book/${book.id}`} className="orderBook">Book</Link>
    }

const showOrderButton = book => {
    return book.order;
}

export default Table;
