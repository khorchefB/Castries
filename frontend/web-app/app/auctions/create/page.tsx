import Heading from '@/app/components/Headings'
import React from 'react'
import AuctionForm from '../AuctionForm'

export default function Create() {
  return (
    <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
        <Heading title='Sell you car!' subtitle='Please enter the details of your car.' />
        <AuctionForm></AuctionForm>
    </div>
  )
}
