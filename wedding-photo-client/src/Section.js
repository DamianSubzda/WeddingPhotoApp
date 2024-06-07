import React from 'react';
import './section.css';

function Section({ children }) {
    return(
        <div className="border">
            {children}
        </div>
    );
}

export default Section;
