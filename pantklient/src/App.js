import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './App.css';

import Routes from './containers/navigation';

class App extends Component {

	renderHeader = () => (
		<div>
			<ul>
				<li>
					<Link to="/"> Home </Link>
				</li>
				<li>
					<Link to="/scan"> Scanner </Link>
				</li>
				<li>
					<Link to="/about"> About </Link>
				</li>
			</ul>
		</div>
	);

  render() {
		return (
			<div>
				{this.renderHeader()}
				<Routes />
			</div>
		);
  }
}

export default App;
