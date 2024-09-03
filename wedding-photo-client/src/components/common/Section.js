import React from 'react';
import './Section.css';

function Section({ children }) {
    return(
        <div className="border">
            {children}
        </div>
    );
}

export default Section;
