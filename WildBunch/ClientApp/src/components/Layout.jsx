import * as React from 'react';
import { useSelector } from 'react-redux';
import { Container } from 'reactstrap';
import { NavMenu } from './Home';

const Layout = (props) => {
    
    return (
        <React.Fragment>
            <NavMenu />
            <Container>
                {props.children}
            </Container>
        </React.Fragment>
    )
}

export default Layout